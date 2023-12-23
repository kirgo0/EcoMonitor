using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class updating_view_for_news : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create or replace the view
            migrationBuilder.Sql(@"
                CREATE VIEW `eco`.`formatted_news` AS
                SELECT 
                    `n`.`id` AS `id`,
                    `n`.`title` AS `title`,
                    `n`.`body` AS `body`,
                    `n`.`post_date` AS `post_date`,
                    `n`.`update_date` AS `update_date`,
                    `n`.`source_url` AS `source_url`,
                    (SELECT 
                            GROUP_CONCAT(`a`.`UserName`
                                    SEPARATOR ',')
                        FROM
                            (`eco`.`NewsUser` `na`
                            JOIN `eco`.`AspNetUsers` `a` ON ((`a`.`Id` = `na`.`authorsId`)))
                        WHERE
                            (`n`.`id` = `na`.`newsid`)) AS `authors`,
                    (SELECT 
                            GROUP_CONCAT(`r`.`name`
                                    SEPARATOR ',')
                        FROM
                            (`eco`.`NewsRegion` `nr`
                            JOIN `eco`.`regions` `r` ON ((`r`.`id` = `nr`.`regionsid`)))
                        WHERE
                            (`n`.`id` = `nr`.`newsid`)) AS `region_names`,
                    (SELECT 
                            GROUP_CONCAT(`c`.`name`
                                    SEPARATOR ',')
                        FROM
                            (`eco`.`CompanyNews` `nc`
                            JOIN `eco`.`companies` `c` ON ((`c`.`id` = `nc`.`companiesid`)))
                        WHERE
                            (`n`.`id` = `nc`.`newsid`)) AS `company_names`,
                    COUNT(`nf`.`likedNewsid`) AS `likes`
                FROM
                    (`eco`.`news` `n`
                    LEFT JOIN `eco`.`NewsFollowers` `nf` ON ((`n`.`id` = `nf`.`likedNewsid`)))
                GROUP BY `n`.`id`, `n`.`title`, `n`.`body`, `n`.`post_date`, `n`.`update_date`;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS `eco`.`formatted_news`;");
        }
    }
}
