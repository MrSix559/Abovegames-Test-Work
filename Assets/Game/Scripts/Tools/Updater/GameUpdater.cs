using System.Collections.Generic;

public interface IUpdate
{
    void OnUpdate();
}

public interface IFixedUpdate
{
    void OnFixedUpdate();
}

public interface ILateUpdate
{
    void OnLateUpdate();
}

namespace Tools
{
    public class GameUpdater : Singleton<GameUpdater>
    {
        private static List<IUpdate> _updateList = new();
        private static List<IFixedUpdate> _fixedUpdateList = new();
        private static List<ILateUpdate> _lateUpdateList = new();
        public static void RegisterUpdate(IUpdate obj) => _updateList.Add(obj);
        public static void RegisterFixedUpdate(IFixedUpdate obj) => _fixedUpdateList.Add(obj);
        public static void RegisterLateUpdate(ILateUpdate obj) => _lateUpdateList.Add(obj);
        public static void UnRegisterUpdate(IUpdate obj) => _updateList.Remove(obj);
        public static void UnRegisterFixedUpdate(IFixedUpdate obj) => _fixedUpdateList.Remove(obj);
        public static void UnRegisterLateUpdate(ILateUpdate obj) => _lateUpdateList.Remove(obj);

        private void Update()
        {
            foreach (var obj in _updateList.ToArray())
                obj.OnUpdate();
        }

        private void LateUpdate()
        {
            foreach (var obj in _lateUpdateList.ToArray())
                obj.OnLateUpdate();
        }

        private void FixedUpdate()
        {
            foreach (var obj in _fixedUpdateList.ToArray())
                obj.OnFixedUpdate();
        }
    }
}