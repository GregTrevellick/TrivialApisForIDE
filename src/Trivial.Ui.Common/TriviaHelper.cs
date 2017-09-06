using System;
using Trivial.Api.Gateway;
using Trivial.Entities;

namespace Trivial.Ui.Common
{
    public static class TriviaHelper
    {
        public static bool ShouldShowTrivia(GeneralOptionsDto generalOptionsDto)
        {
            if (//if weekend and haven't exceeded weekend count
                ((DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday) &&
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekEnd)
                ||
                //if midweek and haven't exceeded midweek count
                ((DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday) &&
                generalOptionsDto.PopUpCountToday < generalOptionsDto.MaximumPopUpsWeekDay))
            {
                // if last popup more than X minutes ago
                if (generalOptionsDto.LastPopUpDateTime < DateTime.Now.AddMinutes(-1 * generalOptionsDto.PopUpIntervalInMins))
                {
                    return true;
                }
            }

            return false;
        }

        public static GeneralOptions2Dto ShowTrivia(AppName appName, string popUpTitle, DateTime lastPopUpDateTime)
        {
            GeneralOptions2Dto generalOptions2Dto = null; 

            var gatewayResponse = RestClient.GetGatewayResponse(appName);
            var popUpBody = Formatter.GetBody(gatewayResponse.Text, gatewayResponse.Attribution);

            if (!string.IsNullOrEmpty(popUpBody))
            {   
                DisplayPopUpMessage(popUpTitle, popUpBody, gatewayResponse.LinkUri);
                generalOptions2Dto = GetGeneralOptions2Dto(lastPopUpDateTime);
            }

            return generalOptions2Dto;
        }

        private static GeneralOptions2Dto GetGeneralOptions2Dto(DateTime lastPopUpDateTime)
        {
            var generalOptions2Dto = new GeneralOptions2Dto();

            var baseDateTime = DateTime.Now;

            //if last pop up was yesterday, then we have gone past midnight, so reset count for today to zero as it is a new day
            if (lastPopUpDateTime.Date < baseDateTime.Date)
            {
                generalOptions2Dto.PopUpCountToday = 1;
            }
            else
            {
                generalOptions2Dto.PopUpCountToday++;
            }

            generalOptions2Dto.LastPopUpDateTime = baseDateTime;

            return generalOptions2Dto;
        }

        //private static void GetGeneralOptionsDto()
        //{
        //    GeneralOptions.IsDirty = true;

        //    var baseDateTime = DateTime.Now;

        //    //if last pop up was yesterday, then we have gone past midnight, so reset count for today to zero as it is a new day
        //    if (GeneralOptions.LastPopUpDateTime.Date < baseDateTime.Date)
        //    {
        //        GeneralOptions.PopUpCountToday == 0;
        //    }

        //    GeneralOptions.PopUpCountToday++;
        //    GeneralOptions.LastPopUpDateTime = baseDateTime;
        //}

        private static void DisplayPopUpMessage(string popUpTitle, string popUpBody, string linkUri)
        {
            var triviaDialog = new TriviaDialog
            {
                Title = popUpTitle,
                AppTextBlock1 = { Text = popUpBody }
            };

            if (!string.IsNullOrEmpty(linkUri))
            {
                triviaDialog.AppHyperlink1.NavigateUri = new Uri(linkUri);
                triviaDialog.AppHyperlink1.Inlines.Clear();
                triviaDialog.AppHyperlink1.Inlines.Add(linkUri);
            }

            triviaDialog.ShowModal();
        }
    }
}
