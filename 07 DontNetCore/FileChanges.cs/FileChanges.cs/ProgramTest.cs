using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class ProgramTest
    {
        public static void Main()
        {
            //ReplaceContent rc = new ReplaceContent(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Models\DTO\",
            //        "f0",
            //        "f1",
            //        false
            //    );
            //rc.Replace();

            //ReplaceClassName rcn = new ReplaceClassName(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Models\DTO\",
            //        "DTO",
            //        false
            //    );
            //rcn.Replace();

            //DeleteWord dw = new DeleteWord(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Models\DTO\",
            //        " (Foreign key)",
            //        false
            //    );
            //dw.Delete();


            //DefaultControllerAndBLRef dcabr = new DefaultControllerAndBLRef(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Controllers\",
            //        false
            //    );
            //dcabr.Update();

            //ReplaceStaticToInstance rc = new ReplaceStaticToInstance(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Controllers\",
            //        false
            //    );
            //rc.Replace();


            //ReplaceContent rc = new ReplaceContent(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo",
            //        "LVE01",
            //        "LVE02",
            //        true
            //    );
            //rc.Replace();

            //ReplaceContent rc = new ReplaceContent(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Controllers\",
            //        @"

            ///// <summary>
            ///// Method used to have consistent (uniform) returns from all controller actions
            ///// </summary>
            ///// <param name=""responseStatusInfo"">ResponseStatusInfo instance containing response specific information</param>
            ///// <returns>Instance of type IHttpActionResult</returns>
            //[NonAction]
            //public IHttpActionResult Returner(ResponseStatusInfo responseStatusInfo)
            //{
            //    if (responseStatusInfo.IsRequestSuccessful)
            //    {
            //        return Ok(ResponseWrapper.Wrap(responseStatusInfo.Message, responseStatusInfo.Data));
            //    }
            //    return ResponseMessage(Request.CreateErrorResponse(responseStatusInfo.StatusCode, responseStatusInfo.Message));
            //}",
            //        "",
            //        true
            //    );
            //rc.Replace();


            //ReplaceContent rc = new ReplaceContent(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Controllers\",
            //        @"ApiController",
            //        "BaseController",
            //        true
            //    );
            //rc.Replace();

            //RemoveReturner rr = new RemoveReturner(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Controllers\",
            //        false
            //    );
            //rr.Remove();

            //ReplaceContent rc = new ReplaceContent(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Controllers\",
            //        @"this.",
            //        "",
            //        true
            //    );
            //rc.Replace();



            //AddAttr addattr = new AddAttr(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Models\DTO\",
            //        false
            //    );
            //addattr.AddJsonPropertyNameAttr();




            //MapDtoToPOCOJsonPropAttr mdtopocAttr = new MapDtoToPOCOJsonPropAttr(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Models\DTO\",
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Models\POCO\",
            //        false
            //    );

            //mdtopocAttr.Map();


            //ModelInjectCreationLastModified injectCreateUpdateProp = new ModelInjectCreationLastModified(
            //        @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Models\POCO\",
            //        false

            //    );
            //injectCreateUpdateProp.Update();


            InjectConsPresaveValidateAddUpdate injectPVAU = new InjectConsPresaveValidateAddUpdate(
                    @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\BL\",
                    false
                );

            injectPVAU.Update();
        }
    }
}
