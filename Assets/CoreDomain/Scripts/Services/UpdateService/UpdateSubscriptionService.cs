using System.Collections.Generic;
using UnityEngine;

namespace CoreDomain.Services
{
    public class UpdateSubscriptionService : MonoBehaviour, IUpdateSubscriptionService
    {
        private List<IUpdatable> _updatablesList = new ();
        private List<IFixedUpdatable> _fixedUpdatablesList = new ();
        
        
        private void Update()
        {
            var numValues = _updatablesList.Count;

            if (numValues <= 0)
            {
                return;
            }

            for (var index = numValues - 1; index > -1; index--)
            {
                var updated = _updatablesList[index];
                updated?.ManagedUpdate();
            }
        }
        
        private void FixedUpdate()
        {
            var numValues = _fixedUpdatablesList.Count;

            if (numValues <= 0)
            {
                return;
            }

            for (var index = numValues - 1; index > -1; index--)
            {
                var updated = _fixedUpdatablesList[index];
                updated?.ManagedFixedUpdate();
            }
        }
        
        public void RegisterUpdatable(IUpdatable updatable)
        {
            if (_updatablesList == null || _updatablesList.Contains(updatable))
            {
                return;
            }

            _updatablesList.Add(updatable);
        }

        public void UnregisterUpdatable(IUpdatable updatable)
        {
            if (_updatablesList == null || !_updatablesList.Contains(updatable))
            {
                return;
            }

            _updatablesList.Remove(updatable);
        }
        
        public void RegisterFixedUpdatable(IFixedUpdatable updatable)
        {
            if (_fixedUpdatablesList == null || _fixedUpdatablesList.Contains(updatable))
            {
                return;
            }

            _fixedUpdatablesList.Add(updatable);
        }

        public void UnregisterFixedUpdatable(IFixedUpdatable updatable)
        {
            if (_fixedUpdatablesList == null || !_fixedUpdatablesList.Contains(updatable))
            {
                return;
            }

            _fixedUpdatablesList.Remove(updatable);
        }
    }
}