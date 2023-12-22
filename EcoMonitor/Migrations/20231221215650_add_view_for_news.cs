using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Migrations
{
    /// <inheritdoc />
    public partial class add_view_for_news : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"
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
                            (`eco`.`newsuser` `na`
                            JOIN `eco`.`aspnetusers` `a` ON ((`a`.`Id` = `na`.`authorsId`)))
                        WHERE
                            (`n`.`id` = `na`.`newsid`)) AS `authors`,
                    (SELECT 
                            GROUP_CONCAT(`r`.`name`
                                    SEPARATOR ',')
                        FROM
                            (`eco`.`newsregion` `nr`
                            JOIN `eco`.`regions` `r` ON ((`r`.`id` = `nr`.`regionsid`)))
                        WHERE
                            (`n`.`id` = `nr`.`newsid`)) AS `region_names`,
                    (SELECT 
                            GROUP_CONCAT(`c`.`name`
                                    SEPARATOR ',')
                        FROM
                            (`eco`.`companynews` `nc`
                            JOIN `eco`.`companies` `c` ON ((`c`.`id` = `nc`.`companiesid`)))
                        WHERE
                            (`n`.`id` = `nc`.`newsid`)) AS `company_names`,
                    COUNT(`nf`.`likedNewsid`) AS `likes`
                FROM
                    (`eco`.`news` `n`
                    LEFT JOIN `eco`.`newsfollowers` `nf` ON ((`n`.`id` = `nf`.`likedNewsid`)))
                GROUP BY `n`.`id` , `n`.`title` , `n`.`body` , `n`.`post_date` , `n`.`update_date`
            ";
            migrationBuilder.Sql(command);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"DROP VIEW formatted_news;";
            migrationBuilder.Sql(command);
        }
    }
}
