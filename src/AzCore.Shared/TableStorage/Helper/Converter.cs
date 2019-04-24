using AzCore.Shared.TableStorage.Entity;

namespace AzCore.Shared.TableStorage.Helper
{
    public static class Converter
    {
        public static ClaimTableEntity ToEntity(this ClaimForm form)
        {
            var target = new ClaimTableEntity();
            target.Description = form.Description;
            target.Id = form.Id;
            target.Images = form.Images;
            target.Label = form.Label;
            target.Name = form.Name;
            target.PolicyNumber = form.PolicyNumber;
            return target;
        }
    }
}
