-- Skrip setup database untuk Thesis Buddy
--  phpMyAdmin atau MySQL command line (CLI)

DROP DATABASE IF EXISTS thesis_buddy;

CREATE DATABASE thesis_buddy;

USE thesis_buddy;

CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL
) ENGINE=InnoDB;

-- Tabel riwayat konsultasi
CREATE TABLE IF NOT EXISTS consultations (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(255),
    timestamp DATETIME,
    answers TEXT,
    recommendations TEXT
) ENGINE=InnoDB;

-- Tabel pertanyaan untuk pertanyaan dinamis/adaptif
CREATE TABLE IF NOT EXISTS questions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    qkey VARCHAR(255),
    prompt TEXT,
    qtype VARCHAR(50),
    options TEXT,
    step INT,
    active TINYINT(1) DEFAULT 1,
    category VARCHAR(20),
    rule_code VARCHAR(50),
    cf_value DOUBLE
) ENGINE=InnoDB;

-- Data pertanyaan McClelland akan diimport otomatis dari assets/mcclelland_questionnaire.txt saat aplikasi dijalankan.
