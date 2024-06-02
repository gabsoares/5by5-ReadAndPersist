using Service;

namespace Controller
{
    public class RadarController
    {
        private RadarService _radarService;

        public RadarController()
        {
            _radarService = new RadarService();
        }

        public bool InsertDataToMongo()
        {
            return _radarService.InsertDataToMongo();
        }
    }
}
