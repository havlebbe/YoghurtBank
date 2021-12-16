using Microsoft.EntityFrameworkCore;
using YoghurtBank.Shared.Infrastructure;
using Microsoft.Extensions.Hosting;

namespace YoghurtBank.Shared.Model
{
    public static class SeedExtensions
    {
        public static async Task<IHost> SeedAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<YoghurtContext>();

                await SeedAsync(context);
            }
            return host;
        }
            
        
        private static async Task SeedAsync(YoghurtContext context)
        {
            await context.Database.MigrateAsync();
            var student1 = new Student
            {
                UserName = "Henning",
                CollaborationRequests = new List<CollaborationRequest>(),
                Email = "HenningG@gmail.com"
            };

            var student2 = new Student
            {
                UserName = "Mads",
                CollaborationRequests = new List<CollaborationRequest>(),
                Email = "Minmail@webspeed.dk"
            };

            var student3 = new Student
            {
                UserName = "Sasha",
                CollaborationRequests = new List<CollaborationRequest>(),
                Email = "slarsen@mails.com"
            };

            var super1 = new Supervisor
            {
                CollaborationRequests = new List<CollaborationRequest>(),
                Ideas = new List<Idea>(),
                UserName = "Roberto O'boto",
                Email = "Roboto@university.com"
            };

            var super2 = new Supervisor
            {
                CollaborationRequests = new List<CollaborationRequest>(),
                Ideas = new List<Idea>(),
                UserName = "Morten",
                Email = "Morten@gmail.com"
            };
            

            var Idea1 = new Idea
            {
                Subject = "Algorithms and Data Structures",
                Title = "Algorithmic Problem Solving",
                Description = "In this project, students will be working with lorem ipsum dolor",
                AmountOfCollaborators = 3,
                Creator = super1,
                Open = true,
                Posted = DateTime.UtcNow,
                StartDate = new DateTime(2022, 4, 28).ToUniversalTime(),
                TimeToComplete = new DateTime(2022, 7, 21).ToUniversalTime() - new DateTime(2022, 4, 28).ToUniversalTime(),
                Type = IdeaType.Project
            };

            var Idea2 = new Idea
            {
                Subject = "Work/life balance",
                Title = "Work life balance at ITU",
                Description = "In this project it will be investigated how students at ITU combine work, sparetime and family time",
                AmountOfCollaborators = 9,
                Creator = super2,
                Open = true,
                Posted = DateTime.UtcNow,
                StartDate = new DateTime(2022, 3, 11).ToUniversalTime(),
                TimeToComplete = new DateTime(2022, 5, 21).ToUniversalTime() - new DateTime(2022, 3, 11).ToUniversalTime(),
                Type = IdeaType.Project
            };

            var Idea3 = new Idea
            {
                Subject = "Medicine",
                Title = "Tranquilizers and their effects on persian cats",
                Description = "In this project, students will be....",
                AmountOfCollaborators = 2,
                Creator = super1,
                Open = true,
                Posted = DateTime.UtcNow,
                StartDate = new DateTime(2022, 1, 1).ToUniversalTime(),
                TimeToComplete = new DateTime(2022, 6, 1).ToUniversalTime() - new DateTime(2022, 1, 1).ToUniversalTime(),
                Type = IdeaType.PhD
            };

            var Idea4 = new Idea
            {
                Subject = "Lorem ipsum and its many uses",
                Title = "Amet luctus at, scelerisque a augue.",
                Description = "Proin sed suscipit nisi. Fusce volutpat eros eget consectetur faucibus. Nunc vel accumsan nunc.",
                AmountOfCollaborators = 3,
                Creator = super2,
                Open = true,
                Posted = new DateTime(2021, 10, 31).ToUniversalTime(),
                StartDate = new DateTime(2022, 1, 1).ToUniversalTime(),
                TimeToComplete = new DateTime(2022, 12, 1).ToUniversalTime() - new DateTime(2022, 1, 1).ToUniversalTime(),
                Type = IdeaType.Bachelor
            };

            var Idea5 = new Idea
            {
                Subject = "Lorem ipsum",
                Title = "Suspendisse nisl nisl, imperdiet sit.",
                Description = "In eget dui et tellus accumsan pellentesque. Fusce volutpat eros eget consectetur faucibus.  Praesent id lectus sagittis, condimentum ex ut, porta orci. Mauris gravida sed leo non feugiat. Duis vitae aliquam massa, quis convallis nunc.",
                AmountOfCollaborators = 1,
                Creator = super2,
                Open = true,
                Posted = new DateTime(2021, 9, 15).ToUniversalTime(),
                StartDate = new DateTime(2022, 3, 15).ToUniversalTime(),
                TimeToComplete = new DateTime(2022, 5, 15).ToUniversalTime() - new DateTime(2022, 3, 15).ToUniversalTime(),
                Type = IdeaType.Masters
            };

            var collabRequest1 = new CollaborationRequest
            {
                Requester = student1,
                Requestee = super1,
                Application = "I love data structures sooooo much",
                Status = CollaborationRequestStatus.Waiting,
                Idea = Idea1
            };

            var collabRequest2 = new CollaborationRequest
            {
                Requester = student1,
                Requestee = super1,
                Application = "I find algorithms to be interesting",
                Status = CollaborationRequestStatus.ApprovedBySupervisor,
                Idea = Idea1
            };

            var collabRequest3 = new CollaborationRequest
            {
                Requester = student1,
                Requestee = super2,
                Application = "I think that it would be good to fix some of the problems with work/life balance",
                Status = CollaborationRequestStatus.ApprovedBySupervisor,
                Idea = Idea2
            };

            var collabRequest4 = new CollaborationRequest
            {
                Requester = student3,
                Requestee = super2,
                Application = "LAtin example text is like, so interesting!",
                Status = CollaborationRequestStatus.Declined,
                Idea = Idea4
            };

            var collabRequest5 = new CollaborationRequest
            {
                Requester = student2,
                Requestee = super1,
                Application = "Lol, i take medicines sometimes, so i think i would be fun to work eith it ",
                Status = CollaborationRequestStatus.ApprovedByStudent,
                Idea = Idea3
            };

            var collabRequest6 = new CollaborationRequest
            {
                Requester = student1,
                Requestee = super1,
                Application = "i would really like to work on this project because...",
                Status = CollaborationRequestStatus.Waiting,
                Idea = Idea3
            };
           

            if (!await context.Users.AnyAsync() && !await context.Ideas.AnyAsync() && !await context.CollaborationRequests.AnyAsync())
            {
                context.Users.Add(student1);
                context.Users.Add(student2);
                context.Users.Add(student3);
                context.Users.Add(super1);
                context.Users.Add(super2);
                context.Ideas.Add(Idea1);
                context.Ideas.Add(Idea2);
                context.Ideas.Add(Idea3);
                context.Ideas.Add(Idea4);
                context.Ideas.Add(Idea5);
                context.CollaborationRequests.Add(collabRequest1);
                context.CollaborationRequests.Add(collabRequest2);
                context.CollaborationRequests.Add(collabRequest3);
                context.CollaborationRequests.Add(collabRequest4);
                context.CollaborationRequests.Add(collabRequest5);
                context.CollaborationRequests.Add(collabRequest6);
                await context.SaveChangesAsync();
            }
        }
    }
}




