-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 24 Bulan Mei 2023 pada 15.35
-- Versi server: 10.4.25-MariaDB
-- Versi PHP: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbpazakat`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbakun`
--

CREATE TABLE `tbakun` (
  `idAkun` varchar(6) NOT NULL,
  `username` text NOT NULL,
  `password` text NOT NULL,
  `email` text NOT NULL,
  `nama` text NOT NULL,
  `status` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbakun`
--

INSERT INTO `tbakun` (`idAkun`, `username`, `password`, `email`, `nama`, `status`) VALUES
('adm001', 'admin1', '123', 'ardi@gmail.com', 'Ardiansyah', 'ADMIN'),
('akn002', 'yumi', '123', 'a@gmail.com', 'Starla', 'USER');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbinfaq`
--

CREATE TABLE `tbinfaq` (
  `IdInfaq` int(6) NOT NULL,
  `nama` text NOT NULL,
  `alamat` text NOT NULL,
  `pembayaran` int(15) NOT NULL,
  `pesan` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbinfaq`
--

INSERT INTO `tbinfaq` (`IdInfaq`, `nama`, `alamat`, `pembayaran`, `pesan`) VALUES
(0, 'Ari', 'Juanda', 50000, 'Semoga Berkah'),
(1, 'aaa', 'aaa', 50000, 'aaa');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbjenis`
--

CREATE TABLE `tbjenis` (
  `kodeJenis` int(10) NOT NULL,
  `namaJenis` text NOT NULL,
  `jumlah` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbjenis`
--

INSERT INTO `tbjenis` (`kodeJenis`, `namaJenis`, `jumlah`) VALUES
(1, 'Beras', '2,5 Kg'),
(2, 'Tunai', 'Rp 45.000');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbmanajemen`
--

CREATE TABLE `tbmanajemen` (
  `id` varchar(6) NOT NULL,
  `kodejenis` int(10) NOT NULL,
  `nama` text NOT NULL,
  `alamat` text NOT NULL,
  `noHp` int(20) NOT NULL,
  `Jumlah` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbmanajemen`
--

INSERT INTO `tbmanajemen` (`id`, `kodejenis`, `nama`, `alamat`, `noHp`, `Jumlah`) VALUES
('mjn001', 1, 'Ardi', 'Aws', 123, '2,5 Kg'),
('mjn002', 1, 'Ardi', 'Aws', 123, '2,5 Kg');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbqurban`
--

CREATE TABLE `tbqurban` (
  `IdQurban` varchar(6) NOT NULL,
  `nama` text NOT NULL,
  `alamat` text NOT NULL,
  `pembayaran` int(15) NOT NULL,
  `pesan` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbqurban`
--

INSERT INTO `tbqurban` (`IdQurban`, `nama`, `alamat`, `pembayaran`, `pesan`) VALUES
('001', 'Panglo', 'Handil', 14000000, 'Semoga Berkah Selalu'),
('QBN001', 'Ari', 'Juanda', 14000000, 'Semoga Berkah Selalu'),
('QBN002', 'Ardi', 'Aws', 14000000, 'Semoga Berkah Selalu');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `tbakun`
--
ALTER TABLE `tbakun`
  ADD PRIMARY KEY (`idAkun`);

--
-- Indeks untuk tabel `tbinfaq`
--
ALTER TABLE `tbinfaq`
  ADD PRIMARY KEY (`IdInfaq`);

--
-- Indeks untuk tabel `tbjenis`
--
ALTER TABLE `tbjenis`
  ADD PRIMARY KEY (`kodeJenis`);

--
-- Indeks untuk tabel `tbmanajemen`
--
ALTER TABLE `tbmanajemen`
  ADD PRIMARY KEY (`id`);

--
-- Indeks untuk tabel `tbqurban`
--
ALTER TABLE `tbqurban`
  ADD PRIMARY KEY (`IdQurban`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `tbjenis`
--
ALTER TABLE `tbjenis`
  MODIFY `kodeJenis` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
