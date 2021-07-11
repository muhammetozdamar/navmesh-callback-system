namespace NavMeshCallbackSystem
{
    public static class NavMeshAgentExtensions
    {
        public static T OnPathStart<T>(this T t, NavMeshAgentCallback action) where T : TrackableNavMeshAgent
        {
            t.onPathStart = action;
            return t;
        }
        public static T OnPathChanged<T>(this T t, NavMeshAgentCallback action) where T : TrackableNavMeshAgent
        {
            t.onPathChanged = action;
            return t;
        }
        public static T OnPathComplete<T>(this T t, NavMeshAgentCallback action) where T : TrackableNavMeshAgent
        {
            t.onPathComplete = action;
            return t;
        }
    }
}

