CREATE TABLE Business(
    business_id 	VARCHAR(22),
    name 			VARCHAR(100),
    state 			VARCHAR(2),
    city 			VARCHAR(100),
    address 		VARCHAR(100),
    zipcode 		INT,
	latitude 		FLOAT,
    longitude 		FLOAT,
    stars 			FLOAT,  /*Aggregate from tip table*/
    is_open 		BOOL,
    review_count 	INT DEFAULT 0,  /*Aggregate from tip table*/
    review_ratings 	FLOAT DEFAULT 0.0,  /*Aggregate from tip table*/
    numCheckins 	INT DEFAULT 0,  /*Aggregate from checkins table*/
    PRIMARY KEY (Business_id)
);

CREATE TABLE Users(
    user_id 		VARCHAR(22),
	name 			VARCHAR(20),
    average_stars 	FLOAT, /*Aggregate from tip table*/
    fans 			INT,
    cool 			INT, /*Aggregate from tip table*/
    funny 			INT, /*Aggregate from tip table*/
    useful 			INT, /*Aggregate from tip table*/
	totallikes		INT, /*Aggregate from tip table*/
    tipCount 		INT, /*Aggregate from tip table*/
    yelping_since 	TIMESTAMP,
    user_latitude 	FLOAT,
    user_longitude 	FLOAT,
    PRIMARY KEY (User_id)
);

CREATE TABLE Hours(
	business_id		CHAR(22),
	day_of_week		VARCHAR(10),
	open			TIME,
	close			TIME,
	PRIMARY KEY (business_id, day_of_week),
	FOREIGN KEY (business_id) REFERENCES Business (business_id)
);

CREATE TABLE Attributes(
	attr_name		VARCHAR,
	business_id		VARCHAR(22),
	value			VARCHAR,
	PRIMARY KEY (attr_name, business_id),
	FOREIGN KEY (business_id) REFERENCES Business (business_id)
);

CREATE TABLE Categories(
	category 		VARCHAR(255),
	business_id 	VARCHAR(22),
	PRIMARY KEY (category, business_id),
	FOREIGN KEY (business_id) REFERENCES Business(business_id)
);

CREATE TABLE Checkins(
		business_id VARCHAR(22),
		date		TIMESTAMP,  /*YYYY-MM-DD HH:MM:SS*/
		PRIMARY KEY (business_id, date),
		FOREIGN KEY (business_id) REFERENCES Business(business_id)
);

CREATE TABLE friend(
	user_id 		VARCHAR(22),
	friend_id 		VARCHAR(22),
	PRIMARY KEY(user_id, friend_id),
	FOREIGN KEY (user_id) REFERENCES Users(user_id),
	FOREIGN KEY (friend_id) REFERENCES Users(user_id)
);

CREATE TABLE tip(
	tipDate			TIMESTAMP, /*YYYY-MM-DD HH:MM:SS*/
	business_id		VARCHAR(22),
	user_id			VARCHAR(22),
	text			VARCHAR,
	likes			INT DEFAULT 0,
	PRIMARY KEY (tipDate, business_id, user_id),
	FOREIGN KEY (business_id) REFERENCES Business(business_id),
	FOREIGN KEY (user_id) REFERENCES Users(user_id)
);