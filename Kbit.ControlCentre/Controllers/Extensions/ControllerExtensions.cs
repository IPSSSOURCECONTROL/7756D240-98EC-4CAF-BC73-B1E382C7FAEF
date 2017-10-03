using System.Collections.Generic;
using Kbit.ControlCentre.Models;

namespace Kbit.ControlCentre.Controllers.Extensions
{
    public static class ControllerExtensions
    {
        public static void BindListsToUserVm(this ViewEditUserVm viewModel)
        {
            viewModel.Password = UserPasswordConstants.MaskPasswordValue;
            viewModel.ConfirmationPassword = UserPasswordConstants.MaskPasswordValue;
            viewModel.Roles = new List<string>
            {
                "Limited Role",
                "Superman Role",
                "Administrator Role",
                "Manager Role",
                "No Role"
            };
            viewModel.LicenseSpecifications = new List<string>
            {
                "Annual License Specification",
                "Demo License Specification",
                "Expired License Specification",
                "Monthly License Specification",
                "Perpertual License Specification"
            };
            viewModel.PasswordResetPolicies = new List<string>
            {
                "Daily Password Reset Policy",
                "Monthly Password Reset Policy",
                "Never Reset Password Reset Policy",
                "Weekly Password Reset Policy"
            };
            viewModel.AccountStatuss = new List<string>
            {
                "Active Account Status",
                "Blocked Account Status",
                "Inactive Account Status"
            };
        }
    }
}