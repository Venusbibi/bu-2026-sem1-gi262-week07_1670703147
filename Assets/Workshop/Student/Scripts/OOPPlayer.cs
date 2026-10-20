using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Solution
{
    public class OOPPlayer : Character
    {
        public Inventory inventory;
        public ActionHistoryManager actionHistoryManager;
        private InputAction moveAction;
        private InputAction fireAction;

        public bool isAutoMoving = false;

        public override void SetUP()
        {
            base.SetUP();
            PrintInfo();
            GetRemainEnergy();
            inventory = GetComponent<Inventory>();

            if (actionHistoryManager == null)
            {
                actionHistoryManager = GetComponent<ActionHistoryManager>();
            }

            moveAction = InputSystem.actions.FindAction("Move");
            fireAction = InputSystem.actions.FindAction("Attack");

            if (actionHistoryManager != null)
            {
                actionHistoryManager.SetUPStartPosition(this);
            }
        }

        public void Update()
        {
            if (isAutoMoving) return;

            if (moveAction.triggered)
            {
                Move(moveAction.ReadValue<Vector2>());
            }

            Keyboard keyboard = Keyboard.current;
            bool firePressed = fireAction.triggered;
            if (keyboard != null)
            {
                firePressed |= keyboard.eKey.wasPressedThisFrame;
            }

            if (firePressed)
            {
                UseFireStorm();
            }

            if (keyboard == null) return;

            if (keyboard.zKey.wasPressedThisFrame && actionHistoryManager != null)
            {
                actionHistoryManager.UndoLastMove(this);
            }

            if (keyboard.qKey.wasPressedThisFrame && actionHistoryManager != null)
            {
                actionHistoryManager.StartAutoMoveSequence(this);
            }
        }

        public override void Move(Vector2 direction)
        {
            base.Move(direction);
            mapGenerator.MoveEnemies();

            Vector2 newPosition = new Vector2(this.positionX, this.positionY);
            if (actionHistoryManager != null)
            {
                actionHistoryManager.SaveStateForUndo(newPosition);
            }
        }

        public void UseFireStorm()
        {
            if (inventory.HasItem("FireStorm", 1))
            {
                inventory.UseItem("FireStorm", 1);
                OOPEnemy[] enemies = SortEnemiesByRemainningEnergy1();
                int count = Math.Min(3, enemies.Length);

                for (int i = 0; i < count; i++)
                {
                    enemies[i].TakeDamage(10);
                }
            }
            else
            {
                Debug.Log("No FireStorm in inventory");
            }
        }

        public OOPEnemy[] SortEnemiesByRemainningEnergy1()
        {
            var enemies = mapGenerator.GetEnemies();

            for (int i = 0; i < enemies.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < enemies.Length; j++)
                {
                    if (enemies[j].energy < enemies[minIndex].energy)
                    {
                        minIndex = j;
                    }
                }
                var temp = enemies[i];
                enemies[i] = enemies[minIndex];
                enemies[minIndex] = temp;
            }

            return enemies;
        }

        public OOPEnemy[] SortEnemiesByRemainningEnergy2()
        {
            var enemies = mapGenerator.GetEnemies();
            Array.Sort(enemies, (a, b) => a.energy.CompareTo(b.energy));
            return enemies;
        }

        public void Attack(OOPEnemy _enemy)
        {
            _enemy.TakeDamage(AttackPoint);
            Debug.Log(_enemy.name + " is energy " + _enemy.energy);
        }

        protected override void CheckDead()
        {
            base.CheckDead();
            if (energy <= 0)
            {
                Debug.Log("Player is Dead");
            }
        }

        #region Sorting Assignment Methods

        public void LCT01_SelectionSortAscending(int[] numbers)
        {
            int n = numbers.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (numbers[j] < numbers[minIndex]) minIndex = j;
                }
                int temp = numbers[minIndex];
                numbers[minIndex] = numbers[i];
                numbers[i] = temp;
            }
            foreach (int num in numbers) Debug.Log(num);
        }

        public void LCT02_BubbleSortAscending(int[] numbers)
        {
            int n = numbers.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        int temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
            foreach (int num in numbers) Debug.Log(num);
        }

        public void LCT03_InsertionSortAscending(int[] numbers)
        {
            int n = numbers.Length;
            for (int i = 1; i < n; i++)
            {
                int key = numbers[i];
                int j = i - 1;
                while (j >= 0 && numbers[j] > key)
                {
                    numbers[j + 1] = numbers[j];
                    j--;
                }
                numbers[j + 1] = key;
            }
            foreach (int num in numbers) Debug.Log(num);
        }

        public void AS01_SelectionSortDescending(int[] numbers)
        {
            int n = numbers.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int maxIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (numbers[j] > numbers[maxIndex]) maxIndex = j;
                }
                int temp = numbers[maxIndex];
                numbers[maxIndex] = numbers[i];
                numbers[i] = temp;
            }
            foreach (int num in numbers) Debug.Log(num);
        }

        public void AS02_BubbleSortDescending(int[] numbers)
        {
            int n = numbers.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (numbers[j] < numbers[j + 1])
                    {
                        int temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
            foreach (int num in numbers) Debug.Log(num);
        }

        public void AS03_InsertionSortDescending(int[] numbers)
        {
            int n = numbers.Length;
            for (int i = 1; i < n; i++)
            {
                int key = numbers[i];
                int j = i - 1;
                while (j >= 0 && numbers[j] < key)
                {
                    numbers[j + 1] = numbers[j];
                    j--;
                }
                numbers[j + 1] = key;
            }
            foreach (int num in numbers) Debug.Log(num);
        }

        public void AS04_FindTheSecondLargestNumber(int[] numbers)
        {
            if (numbers.Length < 2) return;
            Array.Sort(numbers);
            Array.Reverse(numbers);
            int max = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] < max)
                {
                    Debug.Log(numbers[i]);
                    return;
                }
            }
        }

        public void EX01_FindLongestConsecutiveSequence(int[] numbers)
        {
            if (numbers.Length == 0)
            {
                Debug.Log("The longest consecutive sequence is: 0");
                return;
            }
            Array.Sort(numbers);
            int longestStreak = 1, currentStreak = 1;
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] != numbers[i - 1])
                {
                    if (numbers[i] == numbers[i - 1] + 1) currentStreak++;
                    else
                    {
                        longestStreak = Math.Max(longestStreak, currentStreak);
                        currentStreak = 1;
                    }
                }
            }
            longestStreak = Math.Max(longestStreak, currentStreak);
            Debug.Log($"The longest consecutive sequence is: {longestStreak}");
        }

        #endregion
    }
}