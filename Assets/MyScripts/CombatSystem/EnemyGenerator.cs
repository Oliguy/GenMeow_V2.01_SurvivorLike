using JetBrains.Annotations;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

namespace combatSystem
{
    public class EnemyGenerator : MonoBehaviour
    {
        public EnemyDataList_SO enemyDataList_DB;
        public WaveInfoList_SO levelInfoList_DB;
        public RectTransform mapArea;
        public GameObject generatePoint;

        [Header("怪物生成父节点")]
        public GameObject enemyParentPrefab;
        protected GameObject _enemyParent;


        private float _mapWidth;
        private float _mapHeight;

        private int currentWave { get { return GameCopilot.Instance.WaveNow; } set { GameCopilot.Instance.WaveNow = value; } }
        private int timeLeft;
        private float _timer;
        private int waveTime;
        private bool _waveStartbool;
        IEnumerator Start()
        {
            _waveStartbool = false;
            if (_enemyParent!=null)
                Destroy(_enemyParent);
            _enemyParent = Instantiate(enemyParentPrefab);
            yield return new WaitForSeconds(0.2f);
            if (currentWave == 1)
            {
                Debug.Log("游戏开始");
                GenMeowEvent.CallGameStart();
                GenMeowInventoryManager.Instance._inventory.InitSettings();
            }

            _mapWidth = mapArea.rect.width;
            _mapHeight = mapArea.rect.height;
            WaveStart();
            StartEnemyGenerate();
        }

        private void FixedUpdate()
        {
            if (_waveStartbool == false) return;
            _timer -= Time.fixedDeltaTime;
            GetTimeLeft();
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("生成树木" + GetEnemy(9100).gameObject.name);
                StartCoroutine(SingleGenerate(GetGeneratePos(), GetEnemy(9100)));
            }
        }

        public void StartEnemyGenerate()
        {
            foreach(var enemyType in levelInfoList_DB.LevelList[currentWave-1].enemyGenerateInfo)
            {
                StartCoroutine(EnemyGenerate(enemyType));
            }
        }

        /// <summary>
        /// 怪物生成协程，每种怪物一个
        /// </summary>
        /// <param name="_info">由DataCollection定义的生成怪物数据结构：ID、每次数量、每次间隔</param>
        /// <returns></returns>
        IEnumerator EnemyGenerate(EnemyGenerateInfo _info)
        {
            GameObject _enemy = GetEnemy(_info.enemyID);
            while (timeLeft > 0)
            {
                yield return new WaitForSeconds(_info.generateInterval -1f);
                for(int times= 0; times < _info.everyGenerateNum; times++)
                {
                    Vector2 pos = GetGeneratePos();
                    StartCoroutine(SingleGenerate(pos, _enemy));
                }
            }
        }

        IEnumerator SingleGenerate(Vector2 pos,GameObject _enemy)
        {

            Instantiate(generatePoint, new Vector2(pos.x, pos.y), Quaternion.identity,_enemyParent.transform);
            yield return new WaitForSeconds(1f);
            Instantiate(_enemy, new Vector2(pos.x, pos.y), Quaternion.identity, _enemyParent.transform);
        }
        /// <summary>
        /// 返回生成的位置。被注释掉的代码段是地图随机生成，现在只会在边缘生成了。
        /// </summary>
        /// <returns></returns>
        private Vector2 GetGeneratePos()
        {
            #region GPT
            float xPos;
            float yPos;

            bool spawnOnHorizontalEdge = Random.value > 0.5f;
            if (spawnOnHorizontalEdge)
            {
                // Spawn on left or right edge.
                xPos = Random.value > 0.5f ? Random.Range(_mapWidth * 0.35f, _mapWidth * 0.45f) : Random.Range(-_mapWidth * 0.45f, -_mapWidth * 0.35f);
                yPos = Random.Range(-_mapHeight / 2, _mapHeight / 2);
            }
            else
            {
                // Spawn on top or bottom edge.
                xPos = Random.Range(-_mapWidth / 2, _mapWidth / 2);
                yPos = Random.value > 0.5f ? Random.Range(_mapHeight * 0.35f, _mapHeight * 0.45f) : Random.Range(-_mapHeight * 0.45f, -_mapHeight * 0.35f);
            }
            return new Vector2(xPos, yPos);
            #endregion
        }

        //返回Enemy
        public GameObject GetEnemy(int ID)
        {
            return enemyDataList_DB.enemy.Find(i => i.ID == ID).status.gameObject;
        }


        #region WaveControl
        public void WaveStart()
        {
            waveTime = levelInfoList_DB.LevelList[currentWave - 1].LevelTime;
            timeLeft = waveTime;
            _timer = timeLeft;
            _waveStartbool = true;
            GenMeowEvent.CallWaveStart();
        }

        public void WaveEnd()
        {

            if (GameCopilot.Instance.MeowStatus.GetComponent<Health>().CurrentHealth <= 0)
            {
                return;
            }
            StopGenerate();
            if (_enemyParent != null)
                Destroy(_enemyParent);

            MeowLoot[] loots = FindObjectsOfType<MeowLoot>();

            foreach (MeowLoot loot in loots)
            {
                loot.FlyToPlayer();
            }

            _waveStartbool =false;
            GameCopilot.Instance.WaveNow++;
            GenMeowEvent.CallWaveEnd();
        }
        public int GetTimeLeft()
        {
            if (_timer < timeLeft)
            {
                timeLeft--;
                if((waveTime - timeLeft) % 10 == 0)
                {
                    //TODO:概率生成树
                    Debug.Log("生成树木" + GetEnemy(9100).gameObject.name);
                    StartCoroutine(SingleGenerate(GetGeneratePos(), GetEnemy(9100)));
                }
                if ((waveTime - timeLeft - 25) % 15 == 0)
                {
                    //TODO：概率生成大伟丘丘人
                    StartCoroutine(SingleGenerate(GetGeneratePos(), GetEnemy(9000)));
                }
                if (timeLeft <= 0)
                {
                    WaveEnd();
                }
                GenMeowEvent.CallUpdateTime(timeLeft);
            }
            return timeLeft;
        }

        public void StopGenerate()
        {
            StopAllCoroutines();
            CancelInvoke();
        }

        #endregion
    }
}