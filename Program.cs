/*
 * Есть числа вида |X1|X2|X3|X4|X5|X6|M|Y1|Y2|Y3|Y4|Y5|Y7|, где Xn,M,Yn - тринадцатиразрядные.
 * Нужно найти все комбинации таких чисел, у которых X1+X2+...+X6 = Y1+Y2+...Y6.
 * Число M никак не влияет на выполнение условие красивого числа, поэтому 
 * при подсчете красивых чисел его можно не учитывать,
 * а просто в конце домножить результат на 13 - количество возможных вариантов M. 
 * 
 * Сначала нужно перебрать все возможные числа длинною 6 |X1|X2|X3|X4|X5|X6| и посчитать количество возможных
 * комбинаций |X1|X2|X3|X4|X5|X6| для каждой суммы цифр.
 * 
 * Возможные суммы - от 0 (число 000000) до 72 (числа СССССС).
 * Всего чисел |X1|X2|X3|X4|X5|X6| около 13^6 - несколько милилонов, поэтому перебор будет за разумное время.
 * 
 * После перебора необходимо возвести в квадрат количества комбинаций для каждой суммы цифр и сложить их (и домножить на 13).
 * 
 * PS: возможно, количество вариантов чисел для каждого варинта суммы цифр можно посчитать через комбинаторику. 
 * Я не догадался, как (
 */

var combinationsBySum = new Dictionary<int, long>();
for (int i = 0; i <= 72; i++)
    combinationsBySum[i] = 0;
combinationsBySum[0] = 1; // перебор начинвется с 000001, поэтому вручную добавляем вариант 000000

var digitsArray = new int[6];
while (true)
{
    // Console.WriteLine(string.Join("", digitsArray.Select(Convert)));
    digitsArray[5] += 1;
    for(int i = 5; i>=0; i--)
    {
        if (digitsArray[i] > 12)
        {
            digitsArray[i] = 0;
            digitsArray[i - 1] += 1;
        }
        else
            break;
    }

    var sum = digitsArray.Sum();
    combinationsBySum[sum] += 1;

    if(sum == 72) // может быть только одна комбинация CCCCCC
        break;
}

var result = 13 * combinationsBySum.Select(x => x.Value * x.Value).Sum();

Console.WriteLine(result); // 9203637295151

static char Convert(int digit)
{
    return digit switch
    {
        12 => 'C',
        11 => 'B',
        10 => 'A',
        >= 0 and < 10 => (char)('0' | digit),
        _ => throw new ArgumentOutOfRangeException(digit.ToString()),
    };
}
