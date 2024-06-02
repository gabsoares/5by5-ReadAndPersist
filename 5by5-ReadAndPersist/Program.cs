using Controller;

Console.WriteLine(new RadarController().InsertDataToMongo() ? "Sucesso ao inserir" : "Erro");