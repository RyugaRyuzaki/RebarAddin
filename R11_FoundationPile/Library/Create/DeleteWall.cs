using Autodesk.Revit.DB;
using DSP;
namespace R11_FoundationPile
{
    public class DeleteWall
    {
       
        public static void Delete(FoundationPileModel FoundationPileModel, Document document)
        {
            using (Transaction transaction = new Transaction(document))
            {

                transaction.Start("Aa");
                for (int i = 0; i < FoundationPileModel.GroupFoundationModels.Count; i++)
                {
                    for (int j = 0; j < FoundationPileModel.GroupFoundationModels[i].FoundationModels.Count; j++)
                    {
                        if (FoundationPileModel.GroupFoundationModels[i].FoundationModels[j].IsRepresentative)
                        {
                            FoundationPileModel.GroupFoundationModels[i].FoundationModels[j].DeleteWall(document);
                            //FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                            //option.SetFailuresPreprocessor(new DeleteWarningSuper());
                            //transaction.SetFailureHandlingOptions(option);
                        } 
                    }
                    
                }
                transaction.Commit();
            }
        }
        
    }
}
