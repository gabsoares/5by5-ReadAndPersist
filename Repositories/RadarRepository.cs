using Microsoft.Data.SqlClient;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Repositories
{
    public class RadarRepository
    {
        private string _strConnMongo = "mongodb://root:Mongo%402024%23@localhost:27017/";
        private readonly IMongoCollection<BsonDocument> _radarCollection;
        private string _strConnSQL = "Data Source = 127.0.0.1; Initial Catalog=5BY5-PERSIST; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=True;";
        private SqlConnection _conn;
        private SqlCommand _cmd;

        public RadarRepository()
        {
            var client = new MongoClient(_strConnMongo);
            var database = client.GetDatabase("Radar");
            _radarCollection = database.GetCollection<BsonDocument>("DadosRadar");
            _cmd = new();
            _conn = new(_strConnSQL);
            _conn.Open();
        }

        public bool InsertDataToMongo()
        {
            bool result = false;
            string concessionaria = "";
            string anoPNV = "";
            string tipoRadar = "";
            string rodovia = "";
            string UF = "";
            string KM = "";
            string municipio = "";
            string tipoPista = "";
            string sentido = "";
            string situacao = "";
            string dataInativacao = "";
            string latitude = "";
            string longitude = "";
            string velocidadeLeve = "";
            int cont = 1;
            Radar r = new();
            Console.WriteLine("Inserindo dados do SQL no Mongo...");
            try
            {
                _cmd.CommandText = r.GetAll();
                _cmd.Connection = _conn;
                using (SqlDataReader reader = _cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        concessionaria = reader.GetString(0);
                        anoPNV = reader.GetString(1);
                        tipoRadar = reader.GetString(2);
                        rodovia = reader.GetString(3);
                        UF = reader.GetString(4);
                        KM = reader.GetString(5);
                        municipio = reader.GetString(6);
                        tipoPista = reader.GetString(7);
                        sentido = reader.GetString(8);
                        situacao = reader.GetString(9);
                        dataInativacao = reader.GetString(10);
                        latitude = reader.GetString(11);
                        longitude = reader.GetString(12);
                        velocidadeLeve = reader.GetString(13);

                        var document = new BsonDocument
                        {
                            {"_id", cont},
                            {"Concessionaria", concessionaria },
                            {"AnoPNV", anoPNV },
                            {"TipoRadar", tipoRadar },
                            {"Rodovia", rodovia },
                            {"UF", UF },
                            {"KM", KM },
                            {"Municipio", municipio },
                            {"TipoPista", tipoPista },
                            {"Sentido", sentido },
                            {"Situacao", situacao },
                            {"DataInativacao", dataInativacao },
                            {"Latitude", latitude },
                            {"Longitude", longitude },
                            {"VelocidadeLeve", velocidadeLeve }
                        };
                        _radarCollection.InsertOne(document);
                        cont++;
                    }
                    Console.WriteLine("Dados do SQL inseridos no mongo com sucesso!");
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir registros no mongo: " + ex.Source?.ToString());
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }
    }
}
