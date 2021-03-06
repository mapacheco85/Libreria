USE [libreria]
GO
/****** Object:  StoredProcedure [dbo].[PROListarProductos]    Script Date: 25/11/2019 10:44:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 -- exec [PROListarProductos]
ALTER PROCEDURE [dbo].[PROListarProductos]
AS
BEGIN
	SELECT Stock.IdProducto
	    ,Producto.Codigo
		,Producto.Nombre
		,Producto.Descripcion
		,Producto.Foto
		,Stock.CostoVenta
		,IIF(Descuento.Porcentaje IS NULL, Stock.CostoVenta, (Stock.CostoVenta - (Stock.CostoVenta * (Descuento.Porcentaje / 100)))) AS Cobrar
	FROM Producto
	INNER JOIN Stock ON Stock.IdProducto = Producto.IdProducto
	LEFT JOIN Descuento ON Producto.IdProducto = Descuento.IdProducto
	WHERE Stock.Cantidad > 0
END
