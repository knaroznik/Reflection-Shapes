using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace DocX_Export
{
    class Program
    {
        public static string midChar = "_";
        public static char surround = '"';
        public static string[] fieldSeparator = { "\",\"" };

        static void Main(string[] args)
        {
            try
            {
                string filePath = "";
                for (int i = 0; i < args.Length; i++)
                {
                    filePath += args[i] + " ";
                }
                string directoryPath = Path.GetDirectoryName(filePath);
                string[] lines = File.ReadAllLines(filePath);
                Document document = new Document();
                document.AddSection();
                Paragraph paraInserted = document.Sections[0].AddParagraph();
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("-PARAGRAPH-"))
                    {
                        paraInserted = document.Sections[0].AddParagraph();
                        string trueString = lines[i].Substring(12, lines[i].Length - 12);
                        TextRange textRange1 = paraInserted.AppendText("Base type : " + trueString + "\n");
                        textRange1.CharacterFormat.TextColor = Color.Blue;
                        textRange1.CharacterFormat.FontSize = 16;
                        textRange1.CharacterFormat.Bold = true;
                    }
                    else
                    {
                        paraInserted.AppendText("   " + lines[i] + "\n");
                    }
                }
                
                document.SaveToFile(directoryPath + "\\documentation.docx", FileFormat.Doc);
            }
            catch(Exception e)
            {
                Console.Write("ERROR OCCURED : \n\n");
                Console.Write(e.Message);
                Console.Write("\n Press any button to continue...");
                Console.ReadKey();
            }
            
        }

        private static void addTable(Section section, List<string[]> outData, int columns)
        {
            Spire.Doc.Table table = section.AddTable();
            table.ResetCells(outData.Count, columns);

            // ***************** First Row *************************
            TableRow row = table.Rows[0];
            row.IsHeader = true;
            row.Height = 30;    //unit: point, 1point = 0.3528 mm
            row.HeightType = TableRowHeightType.AtLeast;
            row.RowFormat.BackColor = Color.Gray;
            for (int i = 0; i < columns; i++)
            {
                row.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph p = row.Cells[i].AddParagraph();
                p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                TextRange txtRange = p.AppendText(outData[0][i]);
                txtRange.CharacterFormat.Bold = true;
            }

            for (int r = 1; r < outData.Count; r++)
            {
                TableRow dataRow = table.Rows[r];
                dataRow.Height = 20;
                dataRow.HeightType = TableRowHeightType.AtLeast;
                dataRow.RowFormat.BackColor = Color.Empty;
                for (int c = 0; c < columns; c++)
                {
                    dataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    if(outData[r].Length <= c)
                    {
                        dataRow.Cells[c].AddParagraph().AppendText("");
                    }
                    else
                    {
                        if (outData[r][c].Contains("##"))
                        {
                            dataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                            dataRow.Height = 30;
                            dataRow.HeightType = TableRowHeightType.AtLeast;
                            dataRow.RowFormat.BackColor = Color.Gray;

                            TextRange textRange = dataRow.Cells[c].AddParagraph().AppendText(outData[r][c].TrimStart('#'));
                            textRange.CharacterFormat.Bold = true;

                            
                        }
                        else
                        {
                            dataRow.Cells[c].AddParagraph().AppendText(outData[r][c]);
                        }
                    }
                    
                }
            }
        }
    }
}
