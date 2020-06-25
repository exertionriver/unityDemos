CREATE TABLE `players` (
 `id` int(10) NOT NULL AUTO_INCREMENT,
 `username` varchar(32) NOT NULL,
 `pw_hash` varchar(128) NOT NULL,
 `pw_salt` varchar(64) NOT NULL,
 `pb_score` int(10) NOT NULL DEFAULT 0,
 PRIMARY KEY (`id`),
 UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 ;
