//# _MyRepository
//1) Последовательный алгоритм без средств TPL и LINQ.

public static Dictionary<string, int> dic5 = new Dictionary<string, int>();
        public static void consistent_a()
        {
            dirs = Directory.GetFiles(path, "*.txt");
            //DateTime dt, dt2;
            //dt = DateTime.Now;

            for (int i = 0; i < dirs.Length; i++)
            {
                string line;
                string w;
                StreamReader sr = new StreamReader(dirs[i]);
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(new Char[] { ' ', ',', '.', '?', '\\',  '!', '-', '"', ':'}, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < words.Length; j++)
                    {
                        w = words[j];
                        if (dic5.ContainsKey(words[j]))
                            dic5[words[j]]++;
                        else
                            dic5.Add(words[j], 1);

                    }
                }
            }
        }
//2) Алгоритм с применение LINQ-запросов.

static Dictionary<string, int> linq_a(string[] _files)
        {
            var rx1 = new Regex("([A-Z])", RegexOptions.IgnoreCase);
            char[] _delimeters1 = { '.', ',', ':', ';', '?', ' ', '/', '?', '-'};
            var wordCounts = _files
    .SelectMany(path => File.ReadLines(path)
    .SelectMany(line =>
   line.Split(_delimeters1, StringSplitOptions.RemoveEmptyEntries)))
    .Where(word => word.Length > 1)
    .Where(word => rx1.IsMatch(word))
    .GroupBy(word => word)
    .OrderBy(pair => -pair.Count())

    .ToDictionary(group => group.Key,
   group => group.Count());

           


            return (wordCounts);
        }
//3) Алгоритм на базе методов Parallel.For или Parallel.ForEach.

  public static ConcurrentDictionary<string, int> dic = new ConcurrentDictionary<string, int>();
public static void ParallelForEach_a1()
        {
            dirs = Directory.GetFiles(path, "*.txt");
            //DateTime dt, dt2;
            //dt = DateTime.Now;

            Parallel.For(0, dirs.Length, i =>
            {
                string line;
                string w;
                StreamReader sr = new StreamReader(dirs[i]);
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(new Char[] { ' ', ',', '.', '?', '\\', '!', '-', '"', ':' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < words.Length; j++)
                    {
                        w = words[j];
                        dic.AddOrUpdate(w, 1, (key, oldValue) => oldValue + 1);
                    }
                }
            }

            );


        }

//4) Алгоритм с применением PLINQ-запросов.
static Dictionary<string, int> Plinq_a(string[] _files)
        {
            var rx1 = new Regex("([A-Z])", RegexOptions.IgnoreCase);
            char[] _delimeters1 = { '.', ',', ':', ';', '?', ' ', '/', '?', '-' };
            var wordCounts = _files
    .SelectMany(path => File.ReadLines(path)
        .AsParallel()
    .SelectMany(line =>
   line.Split(_delimeters1, StringSplitOptions.RemoveEmptyEntries)))
    .Where(word => word.Length > 1)
    .Where(word => rx1.IsMatch(word))
    .GroupBy(word => word)
    .OrderBy(pair => -pair.Count())

    .ToDictionary(group => group.Key,
   group => group.Count());

            

            return (wordCounts);
        }
//B)	Поиск только 10 самых часто встречающихся слов. Результат работы: string[]
//1)	Последовательный алгоритм без средств TPL и LINQ.

public static Dictionary<string, int> dic6 = new Dictionary<string, int>();

        public static void consistent_b1()
        {
            dirs = Directory.GetFiles(path, "*.txt");
            //DateTime dt, dt2;
            //dt = DateTime.Now;

            for( int j=0; j< dirs.Length; j++)
            {
                string line;
                string w;
                StreamReader sr = new StreamReader(dirs[j]);
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(new Char[] { ' ', ',', '.', '?', '\\',  '!', '-', '"', ':', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < words.Length; k++)
                    {
                        w = words[k];
                        if (dic6.ContainsKey(words[k]))
                                dic6[words[k]]++;
                            else
                                dic6.Add(words[k], 1);

                    }
                }
            }

///2)	Алгоритм с применение LINQ-запросов.

static string[] b_linq(string[] _files)
        {
            var rx = new Regex("([A-Z])", RegexOptions.IgnoreCase);
            Console.WriteLine("b linq");
            Console.WriteLine("10 words");
            char[] _delimeters = { '.', ',', ':', ';', '?', ' ', '/', '?', '-'};

            var wordCounts3 = _files
            .SelectMany(path => File.ReadLines(path)
            .SelectMany(line =>
           line.Split(_delimeters, StringSplitOptions.RemoveEmptyEntries)))
            .Where(word => word.Length > 1)
          .Where(word => rx.IsMatch(word))
            .GroupBy(word => word)
            .OrderByDescending(group => group.Count())
          .Take(10)
          .Select(word => word.Key.ToString()).ToArray();

            return (wordCounts3);
        }
//3)	Алгоритм на базе методов Parallel.For или Parallel.ForEach.
  public static ConcurrentDictionary<string, int> dic2 = new ConcurrentDictionary<string, int>();

        public static void ParallelForEach_b1()
        {
            dirs = Directory.GetFiles(path, "*.txt");
            //DateTime dt, dt2;
            //dt = DateTime.Now;

            Parallel.For(0, dirs.Length, i =>
            {
                string line;
                string w;
                StreamReader sr = new StreamReader(dirs[i]);
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(new Char[] { ' ', ',', '.', '?', '\\', '!', '-', '"', ':'}, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < words.Length; j++)
                    {
                        w = words[j];
                        dic2.AddOrUpdate(w, 1, (key, oldValue) => oldValue + 1);
                    }
                }
            }


            );
            string[] RezultArr = new string[10]; ;
            var myList = dic2.ToList();
            myList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            int counter = 0;
            foreach (KeyValuePair<string, int> kvp in myList)
            {
                if (counter >= 10) { break; }
                else { RezultArr[counter] = kvp.Key; }
                counter++;
            }

          


        }
//4)	Алгоритм с применением PLINQ-запросов.
static string[] Plinq_b(string[] _files)
        {
            var rx = new Regex("([A-Z])", RegexOptions.IgnoreCase);
            Console.WriteLine("b linq");
            Console.WriteLine("10 words");
            char[] _delimeters = { '.', ',', ':', ';', '?', ' ', '/', '?', '-'};

            var wordCounts3 = _files
            .SelectMany(path => File.ReadLines(path)
                .AsParallel()
            .SelectMany(line =>
           line.Split(_delimeters, StringSplitOptions.RemoveEmptyEntries)))
            .Where(word => word.Length > 1)
          .Where(word => rx.IsMatch(word))
            .GroupBy(word => word)
            .OrderByDescending(group => group.Count())
          .Take(10)
          .Select(word => word.Key.ToString()).ToArray();

            return (wordCounts3);
        }
//C)	Распределение числа слов по длине (для построения «гистограммы»). Результат
//работы: Dictionary<int, int>, где ключ – длина слова, значение – число слов.
//1)	Последовательный алгоритм без средств TPL и LINQ.
public static Dictionary<int, int> dic7 = new Dictionary<int, int>();

        public static void consistent_c()
        {
            dirs = Directory.GetFiles(path, "*.txt");
            //DateTime dt, dt2;
            //dt = DateTime.Now;

            Parallel.For(0, dirs.Length, i =>
            {
                string line;
                string w;
                StreamReader sr = new StreamReader(dirs[i]);
                try
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] words = line.Split(new Char[] { ' ',  ',', '<', '>', '\\', '.', '?', '!', '-', '"', '[', ']', '(', ')', '\'', '/', ':' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < words.Length; j++)
                        {
                            w = words[j];
                            if (dic7.ContainsKey(words[j].Length))
                                dic7[words[j].Length]++;
                            else
                                dic7.Add(words[j].Length, 1);

                        }
                    }
                }
                catch(Exception e)
                {

                }

            });
        }
//2)	Алгоритм с применение LINQ-запросов.
        public static Dictionary<int, int> c_linq(string[] _files)
        {
            char[] _delimeters = { '.', ',', ':', ';', '?', ' ', '/', '?', '-'};
            var rx = new Regex("([A-Z])", RegexOptions.IgnoreCase);
            var wordCounts2 = _files
        .SelectMany(path => File.ReadLines(path)
        .SelectMany(line =>
       line.Split(_delimeters, StringSplitOptions.RemoveEmptyEntries)))
        .Where(word => word.Length > 1)

      .Where(word => rx.IsMatch(word))
        .GroupBy(word => word.Length)
        .OrderByDescending(group => group.Count())
        .Take(10)
        .ToDictionary(group => group.Key,
       group => group.Count());
            return (wordCounts2);
        }

//3)	Алгоритм на базе методов Parallel.For или Parallel.ForEach.
public static ConcurrentDictionary<int, int> dic4 = new ConcurrentDictionary<int, int>();

        public static void ParallelForEach_c1()
        {
            dirs = Directory.GetFiles(path, "*.txt");
            //DateTime dt, dt2;
            //dt = DateTime.Now;

            Parallel.For(0, dirs.Length, i =>
            {
                string line;
                string w;
                StreamReader sr = new StreamReader(dirs[i]);
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(new Char[] { ' ', ',', '.', '?', '\\',  '!', '-', '"', ':', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < words.Length; j++)
                    {
                        w = words[j];
                        dic4.AddOrUpdate(w.Length, 1, (key, oldValue) => oldValue + 1);
                    }
                }

            });
        }
//4)	Алгоритм с применением PLINQ-запросов.
public static Dictionary<int, int> Plinq_c(string[] _files)
        {
            char[] _delimeters = { '.', ',', ':', ';', '?', ' ', '/', '?', '-'};
            var rx = new Regex("([A-Z])", RegexOptions.IgnoreCase);
            var wordCounts2 = _files
        .SelectMany(path => File.ReadLines(path)
            .AsParallel()
        .SelectMany(line =>
       line.Split(_delimeters, StringSplitOptions.RemoveEmptyEntries)))
        .Where(word => word.Length > 1)

      .Where(word => rx.IsMatch(word))
        .GroupBy(word => word.Length)
        .OrderByDescending(group => group.Count())
        .Take(10)
        .ToDictionary(group => group.Key,
       group => group.Count());
        
            return (wordCounts2);
        }
 
