using System;
using System.Data;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace Sinqia.Framework.Services
{

    public class CertificadoNaoEncontradoException : Exception
    {

        private const string MensagemDeErro = "O certificado n�o foi encontrado.";


        public CertificadoNaoEncontradoException(string thumbprint)
            : base(MensagemDeErro)
        {
            Thumbprint = thumbprint;
        }

        public CertificadoNaoEncontradoException(string thumbprint, Exception innerException)
            : base(MensagemDeErro, innerException)
        {
            Thumbprint = thumbprint;
        }


        public string Thumbprint { get; private set; }

    }

    /// <summary>
    /// Summary description for Criptografia.
    /// </summary>
    public class Criptografia
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        private const string XML_CertificateThumbprint = "CertificateThumbprint";
        private const string XML_Key = "Key";
        private const string XML_IV = "IV";
        private const int SERIALIZAR = 1;
        private const int DESERIALIZAR = 2;
        private const int AES_KEYSIZE = 256;



        private static char[] convert(byte[] origin)
        {
            if (origin == null || origin.Length < 1) return null;
            int mod = 47;
            char[] ret = new char[origin.Length * 2];
            int converted, low, high;
            int seed = 'A' - 33;
            for (int i = 0; i < origin.Length; i++)
            {
                converted = (origin[i] - 33) ^ seed;
                low = converted % mod;
                high = (int)(converted / mod) + (int)(seed / 2);
                ret[i * 2] = (char)(low + 33);
                ret[i * 2 + 1] = (char)(high + 33);
            }
            return ret;
        }

        private static byte[] unconvert(char[] converted)
        {
            if (converted == null || converted.Length < 2 || (converted.Length % 2) != 0) return null;
            int mod = 47;
            byte[] ret = new byte[converted.Length / 2];
            int unconverted, low, high;
            int seed = 'A' - 33;
            for (int i = 0; i < converted.Length; i += 2)
            {
                low = converted[i] - 33;
                high = converted[i + 1] - 33;
                unconverted = low + (high - (int)(seed / 2)) * mod;
                ret[i / 2] = (byte)((unconverted ^ seed) + 33);
            }
            return ret;
        }

        public static string serialize(string valor)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, valor);
            byte[] b = stream.ToArray();
            stream.Close();
            char[] c = convert(b);
            return new string(c);
        }

        public static string deserialize(string ser)
        {
            IFormatter formatter = new BinaryFormatter();
            string retorno = "";
            char[] c = ser.ToCharArray();
            byte[] b = unconvert(c);

            if (b != null)
            {
                MemoryStream stream = new MemoryStream(b);
                retorno = (string)formatter.Deserialize(stream);
                stream.Close();
            }
            return retorno;
        }



        public static DataSet serializarXMLDataSet(DataSet ds)
        {
            return serializaDeserializeXMLDataSet(ds, SERIALIZAR);
        }

        public static DataSet deserializarXMLDataSet(DataSet ds)
        {
            return serializaDeserializeXMLDataSet(ds, DESERIALIZAR);
        }

        private static DataSet serializaDeserializeXMLDataSet(DataSet ds, int acao)
        {
            var enumeratorRow = ds.Tables["valor"].Rows.GetEnumerator();
            var enumeratorColumn = ds.Tables["valor"].Columns.GetEnumerator();

            while (enumeratorRow.MoveNext())
            {
                var dr = (DataRow)enumeratorRow.Current;
                enumeratorColumn.Reset();
                while (enumeratorColumn.MoveNext())
                {
                    var column = (DataColumn)enumeratorColumn.Current;
                    if (acao == SERIALIZAR)
                        dr[column] = serialize(dr[column].ToString());

                    if (acao == DESERIALIZAR)
                        dr[column] = deserialize(dr[column].ToString());
                }
            }
            ds.AcceptChanges();
            return ds;
        }



        private static void gerarChaveIvAes(out byte[] key, out byte[] iv)
        {
            using (var aes = new AesManaged())
            {
                aes.KeySize = AES_KEYSIZE;
                aes.GenerateIV();
                aes.GenerateKey();

                key = aes.Key;
                iv = aes.IV;
            }
        }

        private static string descriptografarAes(byte[] valor, byte[] key, byte[] iv)
        {
            using (var aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                // Create the streams used for decryption. 
                using (var msDecrypt = new MemoryStream(valor))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt, Encoding))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static byte[] criptografarAes(string valor, byte[] key, byte[] iv)
        {
            using (var aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // Create the streams used for encryption. 
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt, Encoding))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(valor);
                        }

                        return msEncrypt.ToArray();
                    }
                }
            }
        }



        public static void descriptografarXML(XmlDocument xml, X509Certificate2 certificadoExterno, out X509Certificate2 certificadoUtilizado)
        {
            certificadoUtilizado = null;
            X509Certificate2Util certificadoUtil = new X509Certificate2Util();

            if (xml.DocumentElement.Attributes[XML_CertificateThumbprint] != null)
            //if (xml.Tables[0].Columns[XML_CertificateThumbprint] != null) 
            {
                var thumbprint = xml.DocumentElement.Attributes[XML_CertificateThumbprint].InnerText;
                var keyCrypted = Convert.FromBase64String(xml.DocumentElement.Attributes[XML_Key].InnerText);
                var ivCrypted = Convert.FromBase64String(xml.DocumentElement.Attributes[XML_IV].InnerText);

                if (certificadoExterno != null && thumbprint == certificadoExterno.Thumbprint)
                    certificadoUtilizado = certificadoExterno;
                else
                {
                    certificadoUtilizado = certificadoUtil.ObterCertificado(thumbprint);
                    if (certificadoUtilizado == null) throw new CertificadoNaoEncontradoException(thumbprint);
                }


                var key = certificadoUtil.Descriptografar(certificadoUtilizado, keyCrypted);
                var iv = certificadoUtil.Descriptografar(certificadoUtilizado, ivCrypted);

                xml.DocumentElement.InnerXml = descriptografarAes(Convert.FromBase64String(xml.DocumentElement.InnerXml), key, iv);

                // Adiciona os atributos do documento
                foreach (XmlAttribute attribute in xml.DocumentElement.Attributes)
                {
                    //TODO PH: REVER ESSE PONTO
                    //xml.DataSet.ExtendedProperties[attribute.LocalName] = attribute.InnerText;
                }
            }
        }

        public void criptografarXML(XmlDocument xml, X509Certificate2 certificado)
        {
            X509Certificate2Util certificadoUtil = new X509Certificate2Util();
            if (certificado != null)
            {
                // Gera a chave/iv para criptografar os valores
                byte[] key, iv;
                gerarChaveIvAes(out key, out iv);

                // Cria os elementos no xml
                xml.DocumentElement.Attributes.RemoveAll();
                var xmlThumbprint = xml.CreateAttribute(XML_CertificateThumbprint);
                xmlThumbprint.InnerText = certificado.Thumbprint;
                xml.DocumentElement.Attributes.Append(xmlThumbprint);

                var xmlKey = xml.CreateAttribute(XML_Key);
                xmlKey.InnerText = Convert.ToBase64String(certificadoUtil.Criptografar(certificado, key));
                xml.DocumentElement.Attributes.Append(xmlKey);

                var xmlIv = xml.CreateAttribute(XML_IV);
                xmlIv.InnerText = Convert.ToBase64String(certificadoUtil.Criptografar(certificado, iv));
                xml.DocumentElement.Attributes.Append(xmlIv);

                // Criptografa o conteudo do XML
                xml.DocumentElement.InnerXml = Convert.ToBase64String(criptografarAes(xml.DocumentElement.InnerXml, key, iv));
            }
        }
    }


    internal class X509Certificate2Util
    {
        internal X509Certificate2 ObterCertificado(string thumbprint)
        {
            // TODO Gravar traces

            // Busca o certificado da store do usuário.
            var certificado = ObterCertificado(thumbprint, StoreLocation.CurrentUser);


            if (certificado == null)
            {
                certificado = ObterCertificado(thumbprint, StoreLocation.LocalMachine);
            }

            return certificado;
        }


        internal byte[] Criptografar(X509Certificate2 certificado, byte[] valor)
        {
            var publicCsp = certificado.PublicKey.Key as RSACryptoServiceProvider;
            return publicCsp.Encrypt(valor, true);
        }

        internal byte[] Descriptografar(X509Certificate2 certificado, byte[] valor)
        {
            var privateCsp = certificado.PrivateKey as RSACryptoServiceProvider;
            return privateCsp.Decrypt(valor, true);
        }


        X509Certificate2 ObterCertificado(string thumbprint, StoreLocation storeLocation)
        {
            if (string.IsNullOrEmpty(thumbprint)) return null;

            var store = new X509Store(storeLocation);
            store.Open(OpenFlags.ReadOnly);

            var findResult = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

            store.Close();

            return findResult.Count > 0 ? findResult[0] : null;
        }



    }


}
