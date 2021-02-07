using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace MobileDevelopment.UnitTesting.Laboratory1._2_Part1
{
    public class Contents
    {
        private ITestOutputHelper _outputHelper;
        
        public Contents(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        
        private static string GetStudentsString()
        {
            return "Дмитренко Олександр - ІП-84; Матвійчук Андрій - ІВ-83; Лесик Сергій - ІО-82; Ткаченко Ярослав - ІВ-83; Аверкова Анастасія - ІО-83; Соловйов Даніїл - ІО-83; Рахуба Вероніка - ІО-81; Кочерук Давид - ІВ-83; Лихацька Юлія - ІВ-82; Головенець Руслан - ІВ-83; Ющенко Андрій - ІО-82; Мінченко Володимир - ІП-83; Мартинюк Назар - ІО-82; Базова Лідія - ІВ-81; Снігурець Олег - ІВ-81; Роман Олександр - ІО-82; Дудка Максим - ІО-81; Кулініч Віталій - ІВ-81; Жуков Михайло - ІП-83; Грабко Михайло - ІВ-81; Іванов Володимир - ІО-81; Востриков Нікіта - ІО-82; Бондаренко Максим - ІВ-83; Скрипченко Володимир - ІВ-82; Кобук Назар - ІО-81; Дровнін Павло - ІВ-83; Тарасенко Юлія - ІО-82; Дрозд Світлана - ІВ-81; Фещенко Кирил - ІО-82; Крамар Віктор - ІО-83; Іванов Дмитро - ІВ-82";  
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<Student> GetStudents()
        {
            return GetStudentsString()
                .Split(';', StringSplitOptions.TrimEntries)
                .Select(student => Regex.Split(student, @"\ - "))
                .Select(split => new Student {Name = split[0], Group = split[1]})
                .ToList();
        }
        
        #region Task1
        
        [Fact]
        public void Task1()
        {
            // Arrange
            const int expectedIp84 = 1;
            const int expectedIv83 = 6;
            const int expectedIo82 = 7;
            const int expectedIo83 = 3;
            const int expectedIo81 = 4;
            const int expectedIv82 = 3;
            const int expectedIp83 = 2;
            const int expectedIv81 = 5;

            var expectedCount = new [] {
                expectedIv81, expectedIv82, expectedIv83,
                expectedIo81, expectedIo82, expectedIo83,
                expectedIp83, expectedIp84
            };
            
            // Act
            var data = GetStudents().GroupBy(x => x.Group).Select(x => new 
            {
                Group = x.Key,
                Students = x.Select(y => y.Name).ToArray()
            }).OrderBy(x => x.Group);

            var dictionary = data.ToDictionary(i => i.Group, i => i.Students);
            var actual = dictionary.Values.Select(x => x.Length);
            
            // Assert
            Assert.Equal(actual, expectedCount);
        }
        
        #endregion

        #region Task2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetRandomValue(int maxValue)
        {
            var random = new Random();
            return random.Next(6) switch
            {
                1 => (int) Math.Ceiling(maxValue * 0.7),
                2 => (int) Math.Ceiling(maxValue * 0.9),
                3 => maxValue,
                4 => maxValue,
                5 => maxValue,
                _ => 0
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Dictionary<string, int[]> SetPoints(IEnumerable<Student> group, IReadOnlyList<int> points)
        {
            var pointsDict = new Dictionary<string, int[]>();
            var names = group.Select(y => y.Name);
            foreach (var name in names)
            {
                var marks = new int[points.Count];
                for (var i = 0; i < marks.Length; i++)
                {
                    marks[i] = GetRandomValue(points[i]);
                }
                pointsDict.Add(name, marks);
            }
            return pointsDict;
        }

        [Fact]
        public void Task2()
        {
            // Arrange
            var points = new[] {12, 12, 12, 12, 12, 12, 12, 16};
            
            // Act
            var data = GetStudents()
                .GroupBy(x => x.Group)
                .Select(x => new
                {
                    Group = x.Key,
                    Students = SetPoints(x, points)
                });
            
            var dictionary = data.ToDictionary(i => i.Group, i => i.Students);
        }
        
        #endregion

        #region Task3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Dictionary<string, int> SetSumOfPoints(IEnumerable<Student> group, IReadOnlyList<int> points)
        {
            var pointsDict = new Dictionary<string, int>();
            var names = group.Select(y => y.Name);
            foreach (var name in names)
            {
                var marks = new int[points.Count];
                for (var i = 0; i < marks.Length; i++)
                {
                    marks[i] = GetRandomValue(points[i]);
                }
                pointsDict.Add(name, marks.Sum());
            }
            return pointsDict;
        }
        
        [Fact]
        public void Task3()
        {
            // Arrange
            var points = new[] {12, 12, 12, 12, 12, 12, 12, 16};
            
            // Act
            var data = GetStudents()
                .GroupBy(x => x.Group)
                .Select(x => new
                {
                    Group = x.Key,
                    Students = SetSumOfPoints(x, points)
                });
            
            var dictionary = data.ToDictionary(i => i.Group, i => i.Students);
        }
        
        #endregion

        #region Task4
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float SetAvgPointsOfGroup(IEnumerable<Student> group, IReadOnlyList<int> points)
        {
            var names = group.Select(y => y.Name).ToArray();
            float sum = 0;
            var marks = new int[points.Count];
            
            for (var i = 0; i < marks.Length; i++)
            {
                marks[i] = GetRandomValue(points[i]);
            }
            
            sum += marks.Sum();
            return sum / names.Length;
        }
        
        [Fact]
        public void Task4()
        {
            // Arrange
            var points = new[] {12, 12, 12, 12, 12, 12, 12, 16};

            // Act
            var data = GetStudents()
                .GroupBy(x => x.Group)
                .Select(x => new
                {
                    Group = x.Key,
                    AvgPoints = SetAvgPointsOfGroup(x, points)
                });
            
            var dictionary = data.ToDictionary(i => i.Group, i => i.AvgPoints);
        }
        
        #endregion

        #region Task5
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static List<string> SetPointsGreaterThan60(IEnumerable<Student> group, IReadOnlyList<int> points)
        {
            var students = new List<string>();
            var names = group.Select(y => y.Name);
            foreach (var name in names)
            {
                var marks = new int[points.Count];
                for (var i = 0; i < marks.Length; i++)
                {
                    marks[i] = GetRandomValue(points[i]);
                }
                if (marks.Sum() > 60)
                {
                    students.Add(name);
                }
            }
            return students;
        }
        
        [Fact]
        public void Task5()
        {
            // Arrange
            var points = new[] {12, 12, 12, 12, 12, 12, 12, 16};
            
            // Act
            var data = GetStudents()
                .GroupBy(x => x.Group)
                .Select(x => new
                {
                    Group = x.Key,
                    Students = SetPointsGreaterThan60(x, points)
                });
            
            var dictionary = data.ToDictionary(i => i.Group, i => i.Students);
        }
        
        #endregion
    }
}