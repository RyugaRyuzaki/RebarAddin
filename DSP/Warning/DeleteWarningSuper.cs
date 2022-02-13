
using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;

namespace DSP
{
    public class DeleteWarningSuper : IFailuresPreprocessor
    {
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            List<FailureMessageAccessor> failList = failuresAccessor.GetFailureMessages().ToList();
            //if (failList.Count > 0)

            if (failList.Count != 0)
            {
                foreach (FailureMessageAccessor failure in failList)
                {
                    FailureSeverity s = failure.GetSeverity();

                    if (s == FailureSeverity.Warning)
                    {

                        failuresAccessor.DeleteWarning(failure);
                    }
                    else if (s == FailureSeverity.Error)
                    {
                        failuresAccessor.ResolveFailure(failure);
                    }
                }

                return FailureProcessingResult.ProceedWithRollBack;
            }
            else
            {

                return FailureProcessingResult.Continue;
            }
        }
        //public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        //{
        //    IList<FailureMessageAccessor> failList = new List<FailureMessageAccessor>();
        //    // Inside event handler, get all warnings
        //    failList = failuresAccessor.GetFailureMessages();

        //    foreach (FailureMessageAccessor failure in failList)
        //    {
        //        // check FailureDefinitionIds against ones that you want to dismiss, 
        //        FailureDefinitionId failID = failure.GetFailureDefinitionId();
        //        // prevent Revit from showing Unenclosed room warnings
        //        if (failID == BuiltInFailures.RoomFailures.RoomNotEnclosed)
        //        {
        //            failuresAccessor.DeleteWarning(failure);
        //        }
        //    }

        //    return FailureProcessingResult.Continue;
        //}
    }
}
