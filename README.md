# ToDoList (Design DB) #

Database Diagram:

![Database Diagram](https://user-images.githubusercontent.com/107946772/229290693-a6725367-0278-41ea-bcdc-2648649c1b9b.png)

Category table:
~~~~sql
create TABLE category (
	id INT IDENTITY (1, 1) NOT NULL,
	name VARCHAR(64) NOT NULL,
	PRIMARY KEY (id)
)
~~~~

Task table:
~~~~sql
CREATE TABLE task (
	id INT IDENTITY (1, 1) NOT NULL,
	title VARCHAR(128) NOT NULL,
	is_completed BIT DEFAULT 0 NOT NULL,
	due_date DATETIME,
	category_id INT,
	PRIMARY KEY (id),
	FOREIGN KEY (category_id) REFERENCES category(id)
)
~~~~
