using System.ComponentModel;

namespace ExamForms.Constants;

public class Enums
{
    public enum AppRoleEnums
    {
        Admin = 1,
        User = 2,
        Anynomous = 3
    }

    public enum TemplateQuestionTypeEnum
    {
        [Description("Yes/No")]
        Yes_No = 1,

        [Description("Multiple Option")]
        Multiple_Option = 2,

        [Description("Textarea")]
        Text = 3,

        [Description("Multiple Checkbox")]
        Multiple_Checkbox = 4,

        [Description("Text")]
        Date = 5,

        [Description("Email")]
        Email = 6,

        [Description("Phone")]
        Phone = 7,

        [Description("Text Encrypt")]
        TextEncrypt = 8,

        [Description("Number")]
        Number = 9,

        [Description("Multiple Image Option")]
        Multiple_Image_Option = 10,

        [Description("Image Question")]
        Image_Question = 11,

        [Description("True/False")]
        True_False = 12
    }
}
