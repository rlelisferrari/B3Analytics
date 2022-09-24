SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

CREATE TABLE `usuarios` (
  `id_usuario` int(11) NOT NULL,
  `nome` varchar(380) NOT NULL,
  `login` varchar(380) NOT NULL,  
  `email` varchar(380) NOT NULL,
  `senha` varchar(380) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='TABELA USUARIOS';

ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id_usuario`);

ALTER TABLE `usuarios`
  MODIFY `id_usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;
COMMIT;
