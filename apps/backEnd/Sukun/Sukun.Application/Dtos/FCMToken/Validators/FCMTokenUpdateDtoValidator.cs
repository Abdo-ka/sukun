using FluentValidation;
using FluentValidation.Results;
using Sukun.Application.Dtos.BookMark.Responce;
using Sukun.Application.Dtos.City.Response;
using Sukun.Application.Dtos.FCMToken.Request;
using Sukun.Application.Dtos.FCMToken.Response;
using Sukun.Application.Dtos.QuranSurah.Response;
using Sukun.Application.Dtos.QuranVerse.Response;
using Sukun.Application.Dtos.Tafsir.Response;
using Sukun.Application.Dtos.User.Responce;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Dtos.UserDevice.Response;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sukun.Application.Dtos.FCMToken.Validators
{

    public class FCMTokenUpdateDtoValidator : AbstractValidator<FCMTokenUpdateDto>
    {
        public FCMTokenUpdateDtoValidator()
        {
            // Only IsActive is updatable
            RuleFor(x => x.IsActive)
                .NotNull().When(x => x.IsActive.HasValue);
        }
    }

}
