CREATE TABLE IF NOT EXISTS payment (
  Id SERIAL PRIMARY KEY,
  CardNumber varchar(30) NOT NULL,
  CurrencyCode varchar(30) NOT NULL,  
  ExpiryMonth varchar(30) NOT NULL,
  ExpiryYear varchar(30) NOT NULL,
  MerchantId INT NOT NULL,
  StatusCode varchar(30) NOT NULL
);