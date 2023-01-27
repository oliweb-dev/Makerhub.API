using MakerHUB.DAL.Entities;
using MakerHUB.DAL.Tools;
using Microsoft.EntityFrameworkCore;

namespace MakerHUB.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityLiked> ActivityLikeds { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
        public DbSet<MeetingLiked> MeetingLikeds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                   .HasIndex(u => u.Email)
                   .IsUnique();

            builder.Entity<User>()
                   .HasIndex(u => u.Pseudo)
                   .IsUnique();

            LoadUser(builder);
            LoadActivity(builder);
            LoadLikeActivity(builder);
            LoadMeeting(builder);
        }

        private void LoadUser(ModelBuilder builder)
        {
            Utils.CreatePasswordHash("1234=", out byte[] passwordHash, out byte[] passwordSalt);
            Utils.CreatePasswordHash("1234=", out byte[] passwordHash2, out byte[] passwordSalt2);
            Utils.CreatePasswordHash("1234=", out byte[] passwordHash3, out byte[] passwordSalt3);

            builder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Firstname = "Olivier",
                    Lastname = "B",
                    Email = "olivier@gmail.com",
                    Pseudo = "olivier",
                    Image = "aa76be4e-329d-e188-42af-2b5fa2f1260a",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash
                },
                new User
                {
                    UserId = 2,
                    Firstname = "Philippe",
                    Lastname = "D",
                    Email = "philippe@gmail.com",
                    Pseudo = "philippe",
                    Image = "14e155f7-198d-0dda-cb07-7e9ab42b0a46",
                    PasswordSalt = passwordSalt2,
                    PasswordHash = passwordHash2
                },
                new User
                {
                    UserId = 3,
                    Firstname = "Julie",
                    Lastname = "H",
                    Email = "julie@gmail.com",
                    Pseudo = "julie",
                    Image = "9ca30b7b-00e4-0893-9de5-7a65cc10cd8b",
                    PasswordSalt = passwordSalt3,
                    PasswordHash = passwordHash3
                }
            );
        }

        private void LoadActivity(ModelBuilder builder)
        {
            builder.Entity<Activity>().HasData(
                new Activity
                {
                    ActivityId = 1,
                    Date = new DateTime(2022, 12, 20, 10, 15, 30),
                    Title = "Rue van Opré à Jambes",
                    Description = "Ramassage des déchets",
                    UserId = 1,
                    Image = "501e6d67-633a-6d63-4917-71cfd39c1207",
                    Latitude = 50.45744124778758,
                    Longitude = 4.870931169371336,
                },
                new Activity
                {
                    ActivityId = 2,
                    Date = new DateTime(2022, 12, 27, 10, 15, 30),
                    Title = "Rue Mazy à Jambes",
                    Description = "Ramassage des déchets",
                    UserId = 1,
                    Image = "833d9866-40ea-e0f9-973a-0661f7ff1a61",
                    Latitude = 50.46135977534567,
                    Longitude = 4.874223848900589,
                },
                new Activity
                {
                    ActivityId = 3,
                    Date = new DateTime(2022, 12, 1, 10, 15, 30),
                    Title = "Route Merveilleuse à Namur",
                    Description = "Ramassage des déchets",
                    UserId = 1,
                    Image = "67a2e64b-f155-b710-246e-b7765d16bab0",
                    Latitude = 50.459010804528624,
                    Longitude = 4.8634192270445045,
                },
                new Activity
                {
                    ActivityId = 4,
                    Date = new DateTime(2023, 1, 4, 11, 15, 30),
                    Title = "Pont de Jambes",
                    Description = "Ramassage des déchets",
                    UserId = 1,
                    Image = "f379538d-6e67-63c1-d935-8a693a5cfd06",
                    Latitude = 50.45831557105943,
                    Longitude = 4.866099699827113,
                    IsPublic = true,
                },
                new Activity
                {
                    ActivityId = 5,
                    Date = new DateTime(2023, 1, 6, 11, 15, 30),
                    Title = "Le paranoma Citadelle",
                    Description = "Ramassage des déchets",
                    Image = "60cbf3b4-dfe9-d0d3-3d43-43520506cfef",
                    UserId = 1,
                    Latitude = 50.457072552411546,
                    Longitude = 4.859100687392986,
                    IsPublic = true,

                },
                new Activity
                {
                    ActivityId = 6,
                    Date = new DateTime(2023, 1, 15, 11, 15, 30),
                    Title = "Château de Namur",
                    Description = "Ramassage des déchets",
                    Image = "988d4161-b23e-45f7-4962-1bbaff2d9165",
                    UserId = 1,
                    Latitude = 50.45491299318662,
                    Longitude = 4.855394354575543,
                },

                // UserId -> 3
                new Activity
                {
                    ActivityId = 7,
                    Date = new DateTime(2023, 1, 3, 11, 15, 30),
                    Title = "Centre de Ciney",
                    Description = "Ramassage des déchets",
                    Image = "b7124b6c-f782-92e5-c1ae-c7ede7984215",
                    UserId = 3,
                    Latitude = 50.29207313541446,
                    Longitude = 5.0957738631991845,
                    IsPublic = true,
                },
                new Activity
                {
                    ActivityId = 8,
                    Date = new DateTime(2023, 1, 8, 11, 15, 30),
                    Title = "Ciney park Saint-Roch",
                    Description = "Ramassage des déchets",
                    Image = "567db65f-1ad9-a52d-72e8-e2c1f0f10fb8",
                    UserId = 3,
                    Latitude = 50.28871488709505,
                    Longitude = 5.096695784859656,
                    IsPublic = true,
                },
                new Activity
                {
                    ActivityId = 9,
                    Date = new DateTime(2022, 12, 18, 17, 15, 30),
                    Title = "Hastiere grand route",
                    Description = "Ramassage des déchets",
                    Image = "80f82e28-8616-27db-4ac6-09a1715c5a76",
                    UserId = 3,
                    Latitude = 50.1953528932523,
                    Longitude = 4.836934288065565,
                    IsPublic = true,
                },
                new Activity
                {
                    ActivityId = 10,
                    Date = new DateTime(2022, 11, 18, 17, 15, 30),
                    Title = "Viroinvale",
                    Description = "Ramassage des déchets",
                    Image = "342d73c4-0964-da22-90a8-df6ac44766d0",
                    UserId = 3,
                    Latitude = 50.074736140503816,
                    Longitude = 4.60727799119914,
                    IsPublic = true,
                }
            );
        }

        private void LoadLikeActivity(ModelBuilder builder)
        {
            builder.Entity<ActivityLiked>().HasData(
                new ActivityLiked
                {
                    ActivityLikedId = 1,
                    UserId = 1,
                    ActivityId = 1,
                },
                new ActivityLiked
                {
                    ActivityLikedId = 2,
                    UserId = 2,
                    ActivityId = 1,
                },
                new ActivityLiked
                {
                    ActivityLikedId = 3,
                    UserId = 3,
                    ActivityId = 1,
                },
                new ActivityLiked
                {
                    ActivityLikedId = 4,
                    UserId = 2,
                    ActivityId = 2,
                }
            );
        }

        private void LoadMeeting(ModelBuilder builder)
        {
            builder.Entity<Meeting>().HasData(
                    new Meeting
                    {
                        MeetingId = 1,
                        Date = new DateTime(2023, 2, 5, 10, 0, 0),
                        Title = "Sambre de Tamines",
                        Description = "Rendez-vous en dessous du pont de Sambre à 10h",
                        Image = "d3622751-8931-35cd-545c-55bb673bc105",
                        MinParticipant = 2,
                        MaxParticipant = 10,
                        Latitude = 50.43053889673353,
                        Longitude = 4.614097212241228,
                        UserId = 1,
                    },
                    new Meeting
                    {
                        MeetingId = 2,
                        Date = new DateTime(2023, 2, 10, 11, 30, 0),
                        Title = "Ciney",
                        Description = "Rendez-vous près de la gare à 11h30",
                        Image = "567db65f-1ad9-a52d-72e8-e2c1f0f10fb8",
                        MinParticipant = 4,
                        MaxParticipant = 12,
                        Latitude = 50.29139908922892,
                        Longitude = 5.0914419559866975,
                        UserId = 1,
                    },
                    new Meeting
                    {
                        MeetingId = 3,
                        Date = new DateTime(2023, 1, 31, 8, 30, 0),
                        Title = "Ciney Technobel",
                        Description = "Rendez-vous à 8h30",
                        Image = "ef4be8ab-704f-4821-8d79-fcf341f7f498",
                        MinParticipant = 4,
                        MaxParticipant = 12,
                        Latitude = 50.31152890159055,
                        Longitude = 5.107376540644415,
                        UserId = 3,
                    },
                    new Meeting
                    {
                        MeetingId = 4,
                        Date = new DateTime(2022, 12, 15, 10, 30, 0),
                        Title = "Bois de Falisolle",
                        Description = "Rendez-vous à 10h30 près du grand chêne",
                        Image = "3f779eff-6877-7448-be50-7b1fbf8e1d20",
                        MinParticipant = 4,
                        MaxParticipant = 12,
                        Latitude = 50.41672494682598,
                        Longitude = 4.624225466916662,
                        UserId = 3,
                    }
                );
        }
    }
}
