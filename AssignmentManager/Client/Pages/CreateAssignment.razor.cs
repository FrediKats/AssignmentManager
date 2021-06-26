using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AssignmentManager.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace AssignmentManager.Client.Pages
{
    //TODO: move to .cs file
    public partial class CreateAssignment
    {
        string NameFromForm;
        string DescriptionFromForm;
        DateTime? DeadlineFromForm = DateTime.Now;
        string SubjectIdFromForm;

        List<SubjectResourceBriefly> subjects = new List<SubjectResourceBriefly>() {
        new SubjectResourceBriefly() {SubjectId=101, SubjectName="DB" },
        new SubjectResourceBriefly() {SubjectId=102, SubjectName="OS" },
    };

        void OnClick()
        {
            SaveAssignmentResource newAssignment = new SaveAssignmentResource
            {
                Deadline = DeadlineFromForm?.ToString("yyyy-MM-dd HH:mm"),
                Description = DescriptionFromForm,
                Name = NameFromForm,
                SubjectId = int.Parse(SubjectIdFromForm)
            };
            Console.WriteLine(new StringContent(newAssignment.ToString(), Encoding.UTF8, "application/json").ToString());
            //Http.PostAsync("api/Assignments", new StringContent(newAssignment.ToString(), Encoding.UTF8, "application/json"));
            Http.PostAsJsonAsync("api/Assignments", newAssignment);
            DeadlineFromForm = DateTime.Now;
            NameFromForm = "";
            DescriptionFromForm = "";
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            try
            {
                //Console.WriteLine(user.FindFirst(c => c.Type == "IsuId")?.Value);
                int instructorId = int.Parse(user.FindFirst(c => c.Type == "IsuId")?.Value);
                InstructorResource instructor =
                    await Http.GetFromJsonAsync<InstructorResource>($"api/instructors/{instructorId}");
                subjects = instructor?.Subjects.ToList();
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
        }

        bool success;
        string[] errors = { };
    }
}
