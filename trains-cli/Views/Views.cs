

namespace Dr.TrainsCli.Views
{
    public class Views
    {
        internal Views(BaseView baseView, DeparturesView departuresView, StationView stationView)
            => (BaseView, DeparturesView, StationView) = (baseView, departuresView,  stationView);


        public BaseView BaseView { get; private set;}

        public DeparturesView DeparturesView { get; private set;}

        public StationView StationView { get; private set;}
    }
}
