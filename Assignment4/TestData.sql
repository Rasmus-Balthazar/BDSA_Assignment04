INSERT INTO "Users" ("Name", "Email") VALUES ('Anders', 'a@a.com');
INSERT INTO "Users" ("Name", "Email") VALUES ('Bente', 'b@b.com');
INSERT INTO "Users" ("Name", "Email") VALUES ('Carl', 'c@c.com');

INSERT INTO "Tasks" ("title", "AssignedToId", "Description", "State") VALUES ('Search Function', 1, 'do', 'Active');
INSERT INTO "Tasks" ("title", "AssignedToId", "Description", "State") VALUES ('Test', 1, 'do', 'Active');
INSERT INTO "Tasks" ("title", "AssignedToId", "Description", "State") VALUES ('Report', 2, 'do', 'Active');
INSERT INTO "Tasks" ("title", "AssignedToId", "Description", "State") VALUES ('Desgin', 2, 'do', 'Active');
INSERT INTO "Tasks" ("title", "AssignedToId", "Description", "State") VALUES ('Write', 3, 'do', 'Active');

INSERT INTO "Tags" ("Name") VALUES ('x');
INSERT INTO "Tags" ("Name") VALUES ('x');
INSERT INTO "Tags" ("Name") VALUES ('y');
INSERT INTO "Tags" ("Name") VALUES ('y');
INSERT INTO "Tags" ("Name") VALUES ('z');

-- INSERT INTO "TagTask" ("TagsId", "tasksId") VALUES (1, 1);
-- INSERT INTO "TagTask" ("TagsId", "tasksId") VALUES (2, 2);
-- INSERT INTO "TagTask" ("TagsId", "tasksId") VALUES (3, 3);
-- INSERT INTO "TagTask" ("TagsId", "tasksId") VALUES (4, 4);
-- INSERT INTO "TagTask" ("TagsId", "tasksId") VALUES (5, 5);