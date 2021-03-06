using IronXLOld;
using IronXLOld.Styles;

namespace PreviousIronXL
{
    public class TestRunner : TestRunnerBase.TestRunner
    {
        protected override string TestRunnerName => typeof(TestRunner).Namespace ?? "PreviousIronXL";
        protected override void RandomCellsTest(bool savingResultingFile)
        {
            DoTestWork(CreateRandomCells, RandomCellsFileName, savingResultingFile);
        }
        protected override void DateCellsTest(bool savingResultingFile)
        {
            DoTestWork(CreateDateCells, DateCellsFileName, savingResultingFile);
        }
        protected override void StyleChangesTest(bool savingResultingFile)
        {
            DoTestWork(MakeStyleChanges, StyleChangeFileName, savingResultingFile);
        }
        protected override void LoadingBigFileTest(bool savingResultingFile)
        {
            var workbook = WorkBook.Load(BigFileName);
            if (savingResultingFile)
            {
                workbook.SaveAs(LoadingBigFileName);
            }
        }

        private static void DoTestWork(Action<WorkSheet> methodName, string fileName, bool savingResultingFile)
        {
            var workbook = new WorkBook();
            var cells = workbook.DefaultWorkSheet;

            methodName(cells);

            if (savingResultingFile)
            {
                workbook.SaveAs(fileName);
            }
        }

        private static void CreateRandomCells(WorkSheet worksheet)
        {
            var rand = new Random();
            for (int i = 1; i <= RandomCellsRowNumber; i++)
            {
                worksheet["A" + i].Value = $"=\"{Guid.NewGuid()}\"";
                worksheet["B" + i].Value = $"=\"{Guid.NewGuid()}\"";
                worksheet["C" + i].Value = Guid.NewGuid().ToString();
                worksheet["D" + i].Value = rand.Next(32);
                worksheet["E" + i].Value = $"=\"{Guid.NewGuid()}\"";
                worksheet["F" + i].Value = $"=\"{Guid.NewGuid()}\"";
                worksheet["G" + i].Value = Guid.NewGuid().ToString();
                worksheet["H" + i].Value = rand.Next(13);
                worksheet["I" + i].Value = GetRandomDate(rand);
                worksheet["J" + i].Value = GetRandomDate(rand);
                worksheet["K" + i].Value = Guid.NewGuid().ToString();
                worksheet["L" + i].Value = $"=\"{Guid.NewGuid()}\"";
                worksheet["M" + i].Value = Guid.NewGuid().ToString();
                worksheet["N" + i].Value = Guid.NewGuid().ToString();
                worksheet["O" + i].Value = GetRandomDecimal(rand);
                worksheet["P" + i].Value = GetRandomDecimal(rand);
            }
        }

        private static void CreateDateCells(WorkSheet worksheet)
        {
            int dateCellsNumber = 60000;
            for (int i = 1; i < dateCellsNumber; i++)
            {
                worksheet["A" + i].Value = DateTime.Now;
            }
        }

        private static void MakeStyleChanges(WorkSheet worksheet)
        {
            int styleChangeRowNumber = 50;
            worksheet.InsertRows(1, styleChangeRowNumber);

            var range = worksheet.GetRange($"A1:A{styleChangeRowNumber}");
            range.Value = CELL_VALUE;

            var style = range.Style;

            style.Font.Height = 22;
            style.VerticalAlignment = VerticalAlignment.Top;
            style.HorizontalAlignment = HorizontalAlignment.Right;
        }
    }
}