using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Searching
{
    public class Leaderboard : MonoBehaviour
    {
        private List<PlayerScore> scores = new List<PlayerScore>();
        public GameObject UIScore;
        public Transform UiParent;

        void Awake()
        {
            RecordScore(new PlayerScore("Alice", 100));
            RecordScore(new PlayerScore("Bob", 50));
            RecordScore(new PlayerScore("Charlie", 75));
            RecordScore(new PlayerScore("David", 25));
            RecordScore(new PlayerScore("Eve", 125));
            RecordScore(new PlayerScore("Frank", 150));
            RecordScore(new PlayerScore("Lily", 300));
            RecordScore(new PlayerScore("Grace", 175));
            RecordScore(new PlayerScore("Oscar", 375));
            RecordScore(new PlayerScore("Ivan", 225));
            RecordScore(new PlayerScore("Heidi", 200));
            RecordScore(new PlayerScore("Judy", 250));
            RecordScore(new PlayerScore("Kevin", 275));
            RecordScore(new PlayerScore("Nina", 350));
            RecordScore(new PlayerScore("Mona", 325));
        }

        public void RecordScore(PlayerScore score)
        {
            // [1] Sequential search หาผู้เล่นเดิมเพื่อลบออก
            int index = -1;
            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i].playerName == score.playerName)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                scores.RemoveAt(index);
            }

            // [2] Binary search หาตำแหน่งแทรกแบบเรียงลำดับมากไปน้อย
            index = -1;
            int left = 0;
            int right = scores.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (scores[mid].score == score.score)
                {
                    index = mid;
                    break;
                }
                else if (scores[mid].score < score.score)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            if (index == -1)
            {
                index = left;
            }

            scores.Insert(index, score);
        }

        public void PrintScores()
        {
            string allScores = scores.Aggregate("", (acc, score) => acc + score.score.ToString() + ",");
            Debug.Log(allScores);
        }

        public void ShowleaderBoard()
        {
            foreach (var score in scores)
            {
                UIPlayerScore uIScore = Instantiate(UIScore, UiParent).GetComponent<UIPlayerScore>();
                uIScore.SetUpTextScore(score);
            }
        }

        #region Searching Assignment Methods

        public int LCT01_SequentialSearch1DArray()
        {
            int[] array = { 34, 21, 56, 12, 78, 90, 11, 23 };
            int target = 90;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target) return i;
            }
            return -1;
        }

        public int[] LCT02_SequentialSearch2DArray()
        {
            int[,] array = { { 34, 21, 56 }, { 12, 78, 90 }, { 11, 23, 45 } };
            int target = 23;

            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (array[r, c] == target) return new int[] { r, c };
                }
            }
            return new int[] { -1, -1 };
        }

        public int LCT03_BinarySearch()
        {
            int[] array = { 11, 12, 21, 23, 34, 45, 56, 78, 90 };
            int target = 23;

            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (array[mid] == target) return mid;
                if (array[mid] < target) left = mid + 1;
                else right = mid - 1;
            }
            return -1;
        }

        public int[] AS01_FindFirstAndLastElementOfArray(int[] array, int target)
        {
            int first = -1, last = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target)
                {
                    if (first == -1) first = i;
                    last = i;
                }
            }
            if (first == -1) return new int[] { -1 };
            return new int[] { first, last };
        }

        public int AS02_FindMaxLessThan(int[] array, int target)
        {
            int maxVal = int.MinValue;
            bool found = false;
            foreach (int val in array)
            {
                if (val < target)
                {
                    if (!found || val > maxVal)
                    {
                        maxVal = val;
                        found = true;
                    }
                }
            }
            return found ? maxVal : -1;
        }

        public int[] AS03_FindRange(int[] array, int min, int max)
        {
            if (min > max) return new int[0];
            List<int> result = new List<int>();
            foreach (int val in array)
            {
                if (val >= min && val <= max) result.Add(val);
            }
            return result.ToArray();
        }

        public int[] EX01_FindTargetEnemies(int[] enemyHPs, int mana)
        {
            int[] sortedHPs = (int[])enemyHPs.Clone();
            Array.Sort(sortedHPs);
            List<int> selectedEnemies = new List<int>();
            int currentManaUsed = 0;
            foreach (int hp in sortedHPs)
            {
                if (currentManaUsed + hp <= mana)
                {
                    selectedEnemies.Add(hp);
                    currentManaUsed += hp;
                }
                else break;
            }
            return selectedEnemies.ToArray();
        }

        #endregion
    }
}