using Repositories;

namespace Service
{
    public class RadarService
    {
        private RadarRepository _radarRepository;

        public RadarService()
        {
            _radarRepository = new RadarRepository();
        }

        public bool InsertDataToMongo()
        {
            return _radarRepository.InsertDataToMongo();
        }
    }
}
