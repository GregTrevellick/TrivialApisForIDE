﻿using VsixRatingChaser.Dtos;
using VsixRatingChaser.Interfaces;
using static Trivial.Rating.ChaserGateway;

namespace Trivial.Ui.TrumpQuotes
{
    public class PackageRatingChaser
    {
        public void Hunt(IRatingDetailsDto ratingDetailsDto)
        {
            var extensionDetailsDto = new ExtensionDetailsDto
            {
                AuthorName = Vsix.Author,
                ExtensionName = Vsix.Name,
                MarketPlaceUrl = "https://marketplace.visualstudio.com/items?itemName=GregTrevellick.DrainTheSwamp"
            };

            Probe(ratingDetailsDto, extensionDetailsDto);
        }
    }
}