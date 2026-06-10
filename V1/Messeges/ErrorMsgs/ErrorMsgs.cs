using database.models;
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using testDtoAndmapper.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace loginpage.ErrorMsgs
{
    public class IErrorMsgs
    {      
        public static string ACCOUNT_NOT_FOUND = " الحساب غير موجود";
        public static string ACCOUNT_NOT_ACTIVE = "الحساب غير فعال ";
        public static string INVALID_USERANME_OR_PASSWORD = "اسم المستخدم او كلمة المرور خاطئة ";
        public static string SESSION_EXIRED = "انتهاء صلاحية الجلسة ";
        public static string YOUR_ACCOUNT_IS_LOKED = "تم اقفال الحساب ";
        public static string MUST_FILL_ALL_FILLED = "يجب تعبئة الحقول  ";
        public static string NO_DATA_AVAILABLE = "لا توجد بيانات لعرضها"; 
        public static string SREVER_IS_DOWN = "توقف النظام ";
        public static string PAGE_NOT_FOUND = "الصفحة غير متوفرة  ";
        public static string INTERNT_SERVER_ERROR = "مشكلة في الخادم ";
        public static string STUDENTS_NOT_FOUND = "الطالب غير موجود ";
        public static string DOCTOR_NOT_FOUND = "الطبيب غير موجود ";
        public static string DUPLICATE_RECORD_RERRO = "إدخال بيانات مكررة ";
        public static string MAXUMUM_SIZE_OF_GROUP = "المجموعة ممتلئة ";
        public static string NOT_FOUND_DATA = "البيانات غير موجودة ";
        public static string FAILED_TO_SAVE_CHANGES = "مشكلة في تحديث البيانات ";
        public static string INVALID_DATA_FORMAT = "أدخل البيانات بشكل صحيح ";
        public static string SYSTEM_CURRENTLY_UNDER_MAINTENANCE = "النظام قيد الصيانة ";
        public static string CONFLICT_WITH_OTHER_GROUP = "تعارض مع مجموعة اخرى ";
        public static string SCHEDULE_NOT_FINALIZED = "البرنامج لم يعتمد بعد ";
        public static string UNAUTHORIZED_ACCESS_ATTEMPT_DETECTED  = "محاولة دخول غير شرعية إلى النظام ";
        public static string FAILED_TO_SEND_MESSAGE = "مشكلة في ارسال الرسالة ";
        public static string FAILED_ENTER_LEVEL_EXITING_LIMITS = "يجب ادخل المستوى داخل الحدود الموجوده: 4,5,6 ";
        public static string FAILED_NOT_ANY_MAINGROUP = " لم يتم ايجاد اي مجموعة رئيسية على نفس المعرف المرسل ";
        public static string FAILED_GRATE_LEMET = " تجاوزت الحد في توزيع المجموعة الرئيسة على الفترات حيث ان الحد هو توزيع المجموعة على 3 فترات  ";
        public static string FAILED_distributed_to_more_than_one_department = "يجب توزيع المجموعة الرئيسية على قسم واحد على الاقل في كل فترة حيث انك كررت اخيار القسم  ";
        public static string MAXUMUM_SIZE_OF_ROTATION = " لقد تجاوزت الحد المسموح به  حيث ان الحد هو 3 فترات في السنه الواحده ";
        public static string COURSE_ID_NOT_FOUND_IN_COURSE_TABLE = "معرف الكورس غير موجود"; // Added For Attendance Entity from here
        public static string STUDENT_ID_NOT_FOUND_IN_STUDENTS_TABLE = "معرف الطالب غير موجود";
        public static string DOCTOR_ID_NOT_FOUND_IN_DOCTOR_TABLE = "معرف الطبيب غير موجود";
        public static string INVALID_ATTENDANCE_STATUS = "الرجاء ادخال حالة الحضور بشكل صحيح ";
        public static string REQUIRED_ATTENDANCE_DATE = "الرجاء ادخال تاريخ الحضور";
        public static string INVALID_ATTENDANCE_DATE_FUTURE = "لا يمكن أن يكون تاريخ الحضور في المستقبل. الرجاء إدخال تاريخ صحيح";
        public static string INVALID_ATTENDANCE_DATE_FUTURE_OLD = "لا يمكن أن يكون تاريخ الحضور أقدم من شهر. الرجاء إدخال تاريخ حديث";
        public static string STUDENT_ID_IS_NULL = "الرجاء ادخال معرف الطالب";
        public static string DOCTOR_ID_IS_NULL = "الرجاء ادخال معرف الطبيب";
        public static string ATTENDANCE_NOT_FOUND = "لم يتم العثور على حضور الطالب لهذا الكورس";
        public static string INVALID_ATTENDANCE_ID = "معرف الحضور المدخل غير صالح";
        public static string ATTENDANCE_NOT_FOUND_IN_DB = "معرف الحضور غير موجود";
        public static string REPORT_NOT_FOUND = "الرجاء اختيار ملف تقرير"; 
        public static string INVALID_REPORT_ID = "معرف التقرير المدخل غير صالح";
        public static string UPDATE_ERROR = "حدث خطأ اثناء التحديث";
        public static string USER_NOT_FOUND_OR_ROLE_INVALID = "لم يتم العثور على المستخدم أو أنه لا يمتلك الصلاحيات المطلوبة. يرجى التأكد من صحة البيانات المدخلة."; 
        public static string SEARCH_AND_TEACHING_ASSISTANT_ALREADY_EXISTS = "مساعد البحث والتدريس الذي يحمل هذا المعرف موجود بالفعل في النظام. يرجى التحقق من صحة المعرف المدخل."; 
        public static string SUPERVISED_YEAR_INVALID = "السنة المشرف عليها يجب أن تكون 4 أو 5 أو 6. الرجاء التحقق من السنة المدخلة.";
        public static string SEARCH_AND_TEACHING_ASSISTANT_NOT_FOUND = "لم يتم العثور على مساعد البحث والتدريس بهذا المعرف. الرجاء التحقق من البيانات المدخلة.";
        public static string The_material_already_exists = "المادة موجودة بالفعل";
        public static string The_section_does_not_exist = "القسم غير موجود";
        public static string Hospital_with_the_same_name_already_exists = "المستشفى موجود بالفعل";
        public static string The_hospital_does_not_exist = "المستشفى غير موجود ";
        public static string The_doctor_is_already_there = "الطبيب موجود بالفعل";
        public static string The_hospital_does_not_exist_and_The_section_does_not_exist = " القسم غير موجود && المستشفى غير موجود ";
        public static string The_doctor_is_not_present = "الطبيب غير موجود";
        public static string Annual_level_does_not_exist = "المستوى السنوي غير موجود";
        public static string The_material_you_are_trying_to_add_already_exists = "المادة التي تحاول اضافتها موجودة من قبل";
        public static string Article_not_found = "لم يتم العثور على المادة";
        public static string Annual_level_does_not_exist_and_The_section_does_not_exist = "القسم والمستوى السنوي غير صحيحين";
        public static string The_future_does_not_exist = "المستقبل غير موجود ";
        public static string Sender_not_found = "المرسل غير موجود ";

        //****************************************
        public static string The_Material_You_Are_Trying_To_Add_Already_Exists = "المادة التي تحاول إضافتها موجودة بالفعل";
        public static string Annual_Level_And_Department_Do_Not_Exist = "المستوى السنوي والقسم غير موجود";
        public static string It_Is_Not_Acceptable_For_Any_Of_The_Values_To_Be_Empty = " من غير المقبول أن تكون أي من القيم فارغة او صفر";
        public static string The_Code_For_The_Item_You_Are_Trying_To_Add_Already_Exists = " رمز العنصر الذي تحاول إضافته موجود بالفعل";
        public static string The_Code_For_The_Item_You_Are_Trying_To_Add_Already_Exists_And_Year= " رمز العنصر الذي تحاول إضافته موجود بالفعل والنسة غير موجودة ";
        public static string The_Code_For_The_Item_You_Are_Trying_To_Add_Already_Exists_And_Year_And_Departement = " رمز العنصر الذي تحاول إضافته موجود بالفعل والسنة والقسم غير موجودة ";
        public static string The_Dctor_You_Are_Trying_To_Add_Does_Not_Exist_In_The_Users_Table = "الطبيب الذي تحاول إضافته غير موجود في جدول المستخدمين";
        public static string You_Must_Enter_The_Doctors_ID = "يجب عليك إدخال هوية الطبيب";
        public static string Duplicate_Phone_Number = "رقم هاتف مكرر";
        public static string Hospital_Capacity_Must_Be_Greater_Than_0_And_Less_Than_500 = "يجب أن تكون سعة المستشفى أكبر من 0 وأقل من 500";
        public static string There_Are_No_Hospitals = "لا توجد مستشفيات";
        ///
        public static string INVALID_ATTENDANCE_DATE = "تأكد من إدخال تاريخ صحيح (يجب أن يكون من اليوم أو خلال اليومين الماضيين، وألا يكون مستقبليًا أو من سنة سابقة).";
        public static string ATTENDANCE_RECORD_NOT_FOUND = "لم يتم العثور على حضور الطالب لهذا الكورس";
        public static string COURSE_RECORD_NOT_FOUND = "الكورس غير موجود";
        public static string DOCTOR_RECORD_NOT_FOUND = "لم يتم العثور على سجل الدكتور باستخدام المعرف المدخل";
        public static string STUDENT_RECORD_NOT_FOUND = "لم يتم العثور على سجل الطالب باستخدام المعرف المدخل";
        public static string DEPARTMENT_IN_USE = "القسم مستخدم بالفعل";
        public static string DEPARTMENT_NOT_FOUND = "القسم غير موجود"; 
        public static string DEPARTMENT_IS_EXIST = "القسم موجود بالفعل";
        public static string CAN_NOT_DELETE_DEPARTMENT = "لا يمكن حذف هذا القسم";
        public static string DEPARTMENT_ALREADY_EXISTS = "القسم موجود بالفعل";
        public static string DEPARTMENT_DELETE_FAILED = "فشل في حذف القسم";
        public static string NOT_EXIST_SUB_GROUP_WITH_DOCTOR = "لا يوجد بيانات";

        ///*************** USER MUST_FILL_ALL_FILLED
        public static string USER_NOT_FOUND = "المستخدم غير موجود ";
        public static string ROLE_ID_NOT_FOUND = "رقم الرتبة مش موجود ادخل رقم ما بين 1 الى 15 ";
        public static string DUPLICATE_ID_NUMBER = "إدخال بيانات مكررة وهو رقم الهوية ادخل رقم اخر ";
        public static string GENDER_NOT_FOUND = "جنس المستخدم غير مدرج الرجاء ادخل : ذكر او أنثى او male او female ";
        public static string DUPLICATE_Email = "إدخال بيانات مكررة وهو الايميل المدرج مكرر لمستخدم اخر  ";
        public static string DUPLICATE_PHONE_NUMBER = "إدخال بيانات مكررة وهو رقم الهاتف المدخل مكرر لمسخدم اخر   ";
        public static string DUPLICATE_USER_ID = "إدخال بيانات مكررة معرف المستخدم الذي تحاول ادراجه موجود في جدول المستخدمين    ";
        public static string DELETE_ERROR = "حدث خطأ اثناء الحذف ";
        public static string QUESTIONNAIRE_NOT_FOUND = "لا يوجد اي استبيان ";
        public static string SUBGROUP_NOT_FOUND = "لا توجد اي مواعيد تابعة للمجموعة الفرعية هذه  ";
        public static string MAINGROUP_NOT_FOUND = "لا توجد اي مواعيد تابعة للمجموعة الرئيسية هذه  ";
        public static string The_session_end_date_must_be_greater_than_the_session_start_date = "يجب ان يكون التاريخ النهاية الجلسة اكبر من تاريخ بداية الجلسة   ";
        public static string ROTATION_NOT_FOUND = "لا توجد فترات للعرض  ";
        public static string FAILED_DATE = "التاريخ المدخل مكرر في فترة اخرى  ";
        public static string END_DATE_MUST_GRATER = "تاريخ النهاية الفترة يجب ان يكون اكير من تاريخ بداية الفترة   ";
        public static string subgroup_code_must_not_match_the_main_group = " يجب على رمز المجموعة الفرعية ان لا يتطابق مع زمر المجموعة الرئيسية  ";
        public static string NOT_FOUND_MAINGROUP = "المجموعة الرئيسية  غير مدرجة   ";
        public static string NOT_FOUND_SUBGROUP = "المجموعة الرئيسية لا تتحتوي على اي مجموعة فرعية    ";
        public static string NOT_FOUND_SUBGROUP_BYSEARCH = "المجموعة الفرعية غير مدرجة     ";
        public static string NOT_FOUND_AcadimycYear = "السنة الدراسية المدرجة لا تحتوي على أي مجموعة رئيسية  ";
        public static string NOT_FOUND_ANY_DISTRBUTIONMAINGROUP = " لم يتم توزيع المجموعة الرئيسة على الفترات الدراسية ";
        public static string NOT_FOUND_ROTAION = " لا يوجد فترة دراسية تحتوي على نفس المعرف المسند ";
        public static string NOT_FOUND_ALL_ROTAION = " لا يوجد الي فترة دراسية  ";
        public static string NOT_FOUND_DEPARTMENT = " القسم غير مدرج  ";
        public static string NOT_FOUND_ROATATION = " الفترة غير مدرج  ";
        public static string MAXUMUM_NUMBER_OF_APPOTMENTS_IN_ON_ROTATION = "لقد وصلت للحد الاقى في اضافة المواعيد للفترة الواحدة   ";
        public static string STUDENTS_NOT_FOUND_IN_USER_TABLE = " الطالب الذي تحاول اضافته غير موجود في جدول المستخدمين ";
        public static string Students_Role = " الطالب الذي تحاول ادراجة رتبتة لا تتطابق مع رتبة الطالب  ";
        public static string EMPTY_DEPARTMENTS_TABLE = "لا يوجد اي أقسام";
        public static string NOT_FOUND_APPOTMENTS = "لا يوجد اي موعد له نفس المعرف المدرج";
        public static string DUPLICATE_RECORD_APPOTMENTS = "إدخال بيانات مكررة حيث انك قمت بتكرار اضافة المجموعة الفرعية الى نفس الاسبوع اكثر من مرة  ";
        public static string NOT_FOUND_APPOTMENT = "لا توجد اي مواعيد للعرض ";
        public static string NOT_FOUND_DISTBUTION = "لا توجد اي تقسيمات للعرض  ";
        public static string ERROR_ROTATION = "الفترة الدراسية التي تحاول اسنادها تتداخل مع فترة دراسية اخرى";
        public static string ERROR_APPOTMENTS = "الموعد الاسبوعي الذي تحاول اسنادها يتداخل مع موعد اسبوع اخر اخرى  ";
        public static string ERROR_APPOTMENTS_GRATER_THAN_1WEEK = "الموعد الاسبوعي الذي تحاول اسنادها  فترته اكبر من 7 ايام او اصغر من 7 حيث اه هذا الموعد عبارة عن موعد الدوام الاسبوعي   ";
        public static string ERROR_INPUT_DATE = "لا يوجد استبيان للعرض ";
        public static string NOT_FOUND_Questionnaire = "الاستبيان الذي تبحث عنه غير موجود ";
        public static string NOT_FOUND_Data = "ادخل البيانات لان , الحقول فارغة ";
        public static string Error = "حدث خطا غير متوقع";
        public static string NOT_FOUND_MARK = "لا توجد علامات لها نفس المعرف  ";
        public static string Duplicate_Data = " البيانات التي تحاول ادخالها تم ادخالها سابقا تقوم بادخال بيانات مكررة ";
        public static string MARK_NOT_FOUND = " العلامة التي تحاول البحث عنها غير موجودة في السجلات  ";
        public static string OUT_OF_ROTATION_DATE = " التاريخ الذي تحاول ادخال يقع خارج الفترة";
        //Added For WeeklyEvaluation 
        public static readonly string APPOINTMENT_ID_NOT_FOUND_IN_APPOINTMENT_TABLE = "معرف الموعد غير موجود";
        public static readonly string INVALID_TOTAL_POINTS = "مجموع النقاط غير صحيح";
        public static readonly string INVALID_EVALUATION_FORM = "رقم استمارة التقييم خاطئ";
        public static readonly string WEEKLY_EVALUATION_NOT_FOUND = "التقييم الأسبوعي غير موجود";    
        ///evaluation
        public static string The_Question_You_Want_To_Delete_Does_Not_Exist = "السؤال الذي تريد حذفه غير موجود";
        public static string The_Question_You_Are_Trying_To_Uupdate_Does_Not_Exist = "السؤال الذي تحاول تحديثه غير موجود";
        public static string The_Question_You_Want_Does_Not_Exist = "السؤال الذي تريده غير موجود";
        public static string The_Question_You_Are_Trying_To_Add_Already_Exists = "السؤال الذي تحاول إضافته موجود بالفعل";
        //evaluationform
        public static string The_Form_You_Want_To_Delete_Does_Not_Exist = "النموذج الذي تريد حذفه غير موجود";
        public static string The_Form_You_Want_To_Edit_Does_Not_Exist = "النموذج الذي تريد تعديله غير موجود";
        public static string The_Form_You_Want_Does_Not_Exist = "النموذج الذي تريده غير موجود";
        public static string The_Form_You_Want_To_Add_Already_Exists = "النموذج الذي تريد إضافته موجود بالفعل";
        /// evaluationformandevaluationquestion
        public static string The_Requested_Item_Was_Not_Found = "لم يتم العثور على العنصر المطلوب";
        public static string The_Question_You_Want_Does_Not_Exist_In_The_Selected_Form = "السؤال الذي تريده غير موجود في النموذج المحدد";
        //course
        public static string Course_Level_Not_Found = "لم يتم العثور على مستوى الدورة";
        public static string The_Entered_Partition_Is_Not_Included_In_The_Partition_List = "لم يتم تضمين القسم الذي تم إدخاله في قائمة الأقسام";
        public static string The_Entered_Section_Does_Not_Contain_Any_Materials = "القسم الذي تم إدخاله لا يحتوي على أي مواد";
        public static string There_Are_No_Materials_In_This_Level = "لا توجد مواد في هذا المستوى";
        public static string The_Entrance_Section_Does_Not_Contain_Materials_At_This_Level = "القسم المدخل لا يحتوي على مواد في هذا المستوى";
        public static string The_Material_You_Are_Trying_To_Add_Already_Exists_At_The_Same_Level_In_The_Same_Section_And_With_The_Same_Data = "المادة التي تحاول إضافتها موجودة بالفعل، في نفس المستوى، وفي نفس القسم، وبنفس البيانات";
        public static string The_Approved_Academic_Hours_For_The_Subject_Must_Be_More_Than_0 = "يجب أن تكون الساعات الدراسية المعتمدة للمادة أكثر من 0";
        public static string The_Weekly_Rating_Must_Be_Greater_Than_Or_Equal_To_0 = "يجب أن يكون التقييم الأسبوعي أكبر من أو يساوي 0";
        public static string The_Code_You_Are_Trying_To_Add_Has_Already_Been_Added = "كود المادة الذي تحاول اضافته موجود من قبل لمادة اخرى";
        public static string The_Name_Of_The_Item_You_Are_Trying_To_Add_Has_Already_Been_Added = "اسم المادة الذي تحاول اضافته موجود من قبل لمادة اخرى";
        //answertheevaluation
        public static string The_ID_Of_The_Person_Doing_The_Evaluation_Does_Not_Exist = "معرف الشخص الذي يقوم بالتقييم غير موجود";
        public static string The_ID_Of_The_Person_To_Be_Evaluated_Was_Not_Found = "لم يتم العثور على هوية الشخص الذي سيتم تقييمه";
        // Last Edit by Hamza  3/3/2025: 
        public static readonly string TA_NOT_FOUND = "لا يوجد أي قسم بالمعرف المدخل, يرجى التأكد من المعرف المدخل";
        public static readonly string INVALID_QUESTION_ID = "رقم السؤال غير صالح أو غير موجود. يرجى التأكد من الرقم المدخل";
        public static readonly string ATTENDANCE_ALREADY_EXISTS = "لا يمكن تسجيل الحضور مرتين لنفس الطالب في نفس اليوم.";
        public static readonly string INVALID_DATE_RANGE = "تاريخ البداية يجب أن يكون أصغر من تاريخ النهاية";
        public static readonly string NO_ATTENDANCE_RECORDS_FOUND = "لم يتم العثور على أي سجلات حضور في الفترة المحددة";
        public static string NOT_FOUND_APPTOMENTS = "الموعد الاسبوعي الذي تحاول حذفة غير موجود ";
        public static string FAILED_distributed_to_more_than_one_Rotation = "يجب توزيع المجموعة الرئيسية على اكثر من فترة واحده . حيث انك تحاول اضافة نفس المجموعة على نفس الفترة   ";
        public static string DUBLECATE_ROTATON_NAME = "اسم الفترة الدراسية التي تحاول اسنادها مدرج هذا الاسم في فترة اخرى  ";
        public static string DUPLICATE_WEEK_NAME = "اسم الاسبوع الذي تحاول ادراجة موجود بالفعل في الفترة الحالية ";
        public static string NOT_FOUND_ACADEMIC_YEAR = "السنة الدراسية التي تدرجها عنها غير موجودة";
        public static string DUBLICATE_ACADEMIC_YEAR = "السنة الدراسية التي تحاول ادراجها موجودة";
        public static readonly string WEEKLY_EVALUATION_ALREADY_EXIST = "التقييم الأسبوعي الذي تحاول إدخاله موجود مسبقاً, يرجى التأكد من البيانات المدخلة";
        public static string The_Department_Does_Not_Contain_Any_Doctors = "القسم لا يحتوي على أي أطباء";
        public static string MAIN_GROUP_NOT_NULL = "المجموعة الرئيسية تحتوي على طلاب لا تستطيع حذفها ";
        public static string MAIN_GROUP_NOT_NULL_IN_DISTBUTION = "توجد مجموعات داخل المستوا الحالي لكن لم يتم توزيع المجموعات الدراسية على الفترات الدراسية و الأقسام   ";
        public static string DUBLICATE_ROTATION_NAME = " اسم الفترة الدراسية التي تحاول ادراجه مدرج في فترة أخرى   ";
        public static string NOT_FOUND_ANY_DIVISION = " لا توجد اي تقسيمات بحاجة الي الموافقة   ";
        public static string FIALD_INPUT = " البيانات التي تحاول اسنادها خاطئة    ";
        public static string NOT_FOUND_ANY_UnApprove_Division = " لا توجد اي تقسيمات بحاجة لم تتم الموافقة عليها    ";
        public static string NOT_FOUND_ANY_DISTBUTION = " لا توجد اي توزيعات بحاجة الي الموافقة   ";
        public static string ITS_NULL = "";
        public static string DUBLECATE_DISTRBUTION_IN_SAME_WEEK = " تم توزيع المجموعة الفريعة التي اخترتها على هذا الاسبوع من قبل    ";
        public static string The_Form_Does_Not_Contain_Answers= "النموذج لا يحتوي على إجابات";
        public static string The_Doctor_Has_Not_Received_Any_Ratings_Yet= "لم يحصل الطبيب على أي تقييمات حتى الآن";
        public static string The_Doctor_Is_Not_Present_In_This_Form= "الطبيب لم يحصل على اي من عمليات التقييم";
        public static string Model_Not_Found = "النموذج لا يحتوي على اي عمليات تقييم";
        public static string The_Student_Has_Not_Completed_The_Evaluation_Process_Yet= "الطالب لم يقم بعملية التقييم بعد";
        public static readonly string STUDENT_ID_NOT_FOUND = "معرف الطالب غير موجود, الرجاء التحقق من رقم الطالب المدخل"; 
        public static readonly string DOCTOR_ID_NOT_FOUND = "معرف الطبيب غير موجود"; 
        public static readonly string COURSE_ID_NOT_FOUND = "معرف الكورس غير موجود"; 
        public static readonly string NO_ATTENDANCE_LAST_WEEK = "لا يوجد أي سجل حضور بالاسبوع الماضي";
        public static string NOT_FOUND_ANY_MARK = " لا توجد اي علامات بحاجة الي الموافقة   ";
        public static string NOT_FOUND_MARKS = " لا توجد اي علامات لعرضها   ";
        public static string DUBLECATE_MARK = " العلامة التي تحاول ادراجها للطالب تم ادراجها من قبل  ";
        public static string NOT_FOUND_YEAT  = " السنة الدراسية تحاول ادراجها غير موجودة   ";
        public static string DUBLECATE_DATA = " البيانات المدرجة موجودة من قبل   ";
        public static string NOT_FOUND_CORSE_DOCTOR = " معرف الكورس المدرج غير مرتبط مع الطبيب الذي قمت بادراجة    ";
        public static string DUBLECATE_DOCTOR_ABOVE_ONE_COURSE = " الطبيب الذي تحاول ادراجة مشرف على مادة اخرى    ";
        public static string NOT_FOUND_ANY_COURE_AND_DOCTRO = " لا يوجد الي طبيب مرتبط بكورس للعرض    ";
        public static string CONFLICT_DATE = "الموعد الذي تدرجة يتضارب مع موعد الاسابيع الموجدة في هذه الفترة  ";
        public static string NULL_DATA_FOR_POLICIE = " انت تحاول اضافة بيانات فارغة الى السياسات     ";
        public static string NOT_FOUBD_POLICIE = " السياسة التي تحاول البحث عنها غير موجودة  ";
        public static string NOT_FOUND_ANY_STUDNETS_FOR_THE_EVALUATOIN = " لا يوجد اي طالب متاح تستطيع ان تقوم بإجراء له عملية التقييم    ";
        public static string THER_IS_NOT_ANY_EVALUATION_WEEKLY = " لا توجد اي تقيمات اسبوعية خاصة في الطالب المدرج المعرف الخاص به   ";
        public static string NOT_FOUND_LEVEL = "لم يتم إضافة مستوي الدراسي ";
        public static readonly string INVALID_EVALUATION_FORM_NAME = "  لا يوجد أي نموذج بالاسم المدخل, يرجى التأكد من اسم النموذج المدخل";
        public static string CONFLICT_DATE_ROTATION = "الفترة التي تحاول إضافتها موجوده بالفعل";
        public static string OUT_OF_LEMEIT = "موعد الفترة الذي تحاول إسنادة  يتضارب مع الأسابيع الموجودين ضمن هذه الفترة";
        ///////
        public static string You_Have_Oot_Made_Any_Modification_Please_Make_Any_Modifications_For_The_Modification_Process_To_Be_Successful = "لم تُجرِ أي تعديلات. يُرجى إجراء أي تعديلات لضمان نجاح عملية التعديل";
        //////ta
        public static string Research_And_Teaching_Assistant_ID_Not_Found = "لم يتم العثور على معرف مساعد البحث والتدريس";
        public static string THERE_IS_NOT_ATTENTDENTS_TODAY = "اليوم غير في جدول الدوام لتسجيل الحضور و الغياب";

    }
}
