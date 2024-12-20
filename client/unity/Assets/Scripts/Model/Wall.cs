using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BattleCity
{
    public class Wall
    {
        public Position wallPos;
        public GameObject vertWall;
        public GameObject horiWall;
        public GameObject vertFence;
        public GameObject horiFence;

        private GameObject createdWallObject; // ���ڱ��洴����ǽ�����

        private static readonly string[] horiWallPrefabNames =
        {
            "horiWall00",
            "horiWall01",
            "horiWall02",
            "horiWall03",
            "horiWall04"
        };

        private static readonly string[] vertWallPrefabNames =
        {
            "vertWall00",
            "vertWall01",
            "vertWall02"
        };
        public Wall(Position wallpos)
        {
            wallPos = wallpos;
            AssignRandomHoriWall();
            AssignRandomVertWall();
            vertFence = Resources.Load<GameObject>("Prefabs/Wall/vertFence");
            horiFence = Resources.Load<GameObject>("Prefabs/Wall/horiFence");
        }

        public Wall(double X, double Y, double Angle)
        {
            Position position = new(X, Y, Angle);
            wallPos = position;
            AssignRandomHoriWall();
            AssignRandomVertWall();
            vertFence = Resources.Load<GameObject>("Prefabs/Wall/vertFence");
            horiFence = Resources.Load<GameObject>("Prefabs/Wall/horiFence");
        }

        public GameObject CreateWallObject()
        {
            GameObject wallController = GameObject.Find("WallController");
            if (wallPos.Angle == 90)
            {
                Vector3 position = new Vector3((float)((wallPos.X + Constants.WALL_XBIAS) * Constants.FLOOR_LEN), (float)(wallPos.Y + Constants.Y_BIAS), (float)((wallPos.Z ) * Constants.FLOOR_LEN));
                createdWallObject = Object.Instantiate(vertWall, position, Quaternion.identity);
                createdWallObject.transform.SetParent(wallController.transform);
            }
            else if (wallPos.Angle == 0)
            {
                Vector3 position = new Vector3((float)((wallPos.X ) * Constants.FLOOR_LEN), (float)(wallPos.Y + Constants.Y_BIAS), (float)((wallPos.Z + Constants.WALL_ZBIAS) * Constants.FLOOR_LEN));
                createdWallObject = Object.Instantiate(horiWall, position, Quaternion.identity);
                createdWallObject.transform.SetParent(wallController.transform);
            }
            else
            {
                Debug.LogError("The angle of the wall is invalid!");
            }


            return createdWallObject;
        }

        public GameObject CreateFenceObject()
        {
            Vector3 position = new Vector3((float)wallPos.X, (float)wallPos.Y, (float)wallPos.Z);
            if (wallPos.Angle == 90)
            {
                createdWallObject = Object.Instantiate(vertFence, position, Quaternion.identity);
            }
            else if (wallPos.Angle == 0)
            {
                createdWallObject = Object.Instantiate(horiFence, position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("The angle of the fence is invalid!");
            }

            return createdWallObject;
        }

        public void RemoveWall()
        {
            if (createdWallObject != null)
            {
                Object.Destroy(createdWallObject); // ����ǽ�����
                createdWallObject = null; // �������
            }
            else
            {
                Debug.LogWarning("No wall object to remove at this position.");
            }
        }

        private void AssignRandomHoriWall()
        {
            // ���ѡ��һ��Ԥ��������
            int randomIndex = Random.Range(0, horiWallPrefabNames.Length);
            // �����������ض�Ӧ��Ԥ����
            string prefabPath = "Prefabs/Wall/" + horiWallPrefabNames[randomIndex];
            horiWall = Resources.Load<GameObject>(prefabPath);

            if (horiWall == null)
            {
                Debug.LogError("Failed to load prefab: " + prefabPath);
            }
        }
        private void AssignRandomVertWall()
        {
            // ���ѡ��һ��Ԥ��������
            int randomIndex = Random.Range(0, vertWallPrefabNames.Length);
            // �����������ض�Ӧ��Ԥ����
            string prefabPath = "Prefabs/Wall/" + vertWallPrefabNames[randomIndex];
            vertWall = Resources.Load<GameObject>(prefabPath);

            if (vertWall == null)
            {
                Debug.LogError("Failed to load prefab: " + prefabPath);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Wall other)
            {
                return wallPos.Equals(other.wallPos);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return wallPos.GetHashCode();
        }
    }
}
