using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Exceptions;

namespace TestApplication.Lenders
{
    public class Lenders
    {
        private List<LenderDetails> _lenders;

        private Lenders(List<LenderDetails> list)
        {
            _lenders = list;
        }

        public Result<LenderDetails> GetLenderByAmount(decimal amount)
        {
            var selectedLender = _lenders
                .OrderBy(lenders => lenders.Rate)
                .Where(lender => lender.Amount >= amount)
                .FirstOrDefault();
            if (selectedLender == null)
            {
                return Result<LenderDetails>.Error("No lenders available for the requested amount");
            }
            return Result<LenderDetails>.Ok(selectedLender);
        }

        public static Lenders Create(List<LenderDetails> lenders)
        {
            return new Lenders(lenders);
        }

        public static Result<Lenders> LoadLendersFromCsvFile(string csvFileName)
        {
            var filePathResult = ValidFileName(csvFileName);
            if (!filePathResult.IsValid) return Result<Lenders>.Error(filePathResult.Message);

            var fileDataResult = ReadAllLines(filePathResult.Value);
            if (!fileDataResult.IsValid) return Result<Lenders>.Error(fileDataResult.Message);

            List<LenderDetails> lendersList;
            try
            {
                lendersList = fileDataResult.Value
                    .Skip(1) // Ignore the headers
                    .Select(row => LenderDetails.Create(row))
                    .ToList();
            }
            catch (InvalidCsvLendersFileFormatException e)
            {
                return Result<Lenders>.Error(e.Message);
            }
            return Result<Lenders>.Ok(new Lenders(lendersList));
        }

        private static Result<string> ValidFileName(string filename)
        {
            try
            {
                return Result<string>.Ok(Path.GetFullPath(filename));
            }
            catch (Exception e)
            {
                return Result<string>.Error("Invalid CSV file name");
            }
        }

        private static Result<string[]> ReadAllLines(string filename)
        {
            try
            {
                return Result<string[]>.Ok(File.ReadAllLines(filename));
            }
            catch (Exception e)
            {
                return Result<string[]>.Error("Can't read the CSV file");
            }
        }
    }
}
