using TravelExpertsData.Models;

namespace TravelExpertsData.Manager
{
    public class AgentManager
    {
        public static List<Agent> GetAllAgents(TravelExpertsContext db)
        {
            List<Agent> agents = db.Agents.ToList();
            return agents;
        }
    }//class
}//namespace
