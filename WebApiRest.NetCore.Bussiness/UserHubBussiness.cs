using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRest.NetCore.Bussiness
{
    public abstract class UserHubBussiness
    {
        private static Dictionary<int, string> _UsersConnected = new Dictionary<int, string>();

        public void InitHubConnectionId(int id)
        {
            try
            {
                _UsersConnected.Add(id, string.Empty);
            }
            catch
            {
                throw new Exception("User already connected!");
            }
        }

        public bool IsUserConnected(int id)
        {
            return _UsersConnected.ContainsKey(id);
        }

        public bool IsUserConnected(int[] id)
        {
            return _UsersConnected.Keys.ToList().Exists(l => id.Contains(l));
        }

        public void SetHubConnectionId(string hubConnectionId, int id)
        {
            if (_UsersConnected.ContainsKey(id))
                _UsersConnected[id] = hubConnectionId;
        }

        public string GetHubConnectionId(int id)
        {
            if (_UsersConnected.ContainsKey(id))
                return _UsersConnected[id];

            return null;
        }

        public Task<bool> RemoveHubConnectionId(string hubConnectionId, int? id)
        {
            return
                Task.Factory.StartNew<bool>(
                    () =>
                    {
                        try
                        {
                            // Remove by hubConnectionId
                            if (!string.IsNullOrEmpty(hubConnectionId))
                                if (_UsersConnected.ContainsValue(hubConnectionId))
                                {
                                    // Get connection
                                    var connection = _UsersConnected.FirstOrDefault(l => l.Value == hubConnectionId);
                                    // Remove connection
                                    return _UsersConnected.Remove(connection.Key);
                                }
                                else
                                {
                                    return false;
                                }
                            // Remove by id (key)
                            else if (id.HasValue)
                                if (_UsersConnected.ContainsKey(id.Value))
                                    // Remove connection
                                    return _UsersConnected.Remove(id.Value);
                                else
                                    return false;
                            else
                                return false;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                );
        }
    }
}