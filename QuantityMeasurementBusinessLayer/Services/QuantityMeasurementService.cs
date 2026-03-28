using System;
using QuantityMeasurementModelLayer;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementRepositoryLayer;
using QuantityMeasurementBusinessLayer.Interfaces;

namespace QuantityMeasurementBusinessLayer
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repository;

        public QuantityMeasurementService()
        {
        }

        public QuantityMeasurementService(IQuantityMeasurementRepository repository)
        {
            _repository = repository;
        }

        // --- Preserved Core Logic ---
        public bool AreEqual<U>(Quantity<U> first, Quantity<U> second) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return first.Equals(second);
        }

        public Quantity<U> Convert<U>(Quantity<U> quantity, U targetUnit) where U : struct, Enum
            => quantity.Convert(targetUnit);

        public Quantity<U> Add<U>(Quantity<U> first, Quantity<U> second, U targetUnit) where U : struct, Enum
            => Quantity<U>.Add(first, second, targetUnit);

        public Quantity<U> Subtract<U>(Quantity<U> first, Quantity<U> second, U targetUnit) where U : struct, Enum
            => Quantity<U>.Subtract(first, second, targetUnit);

        public double Divide<U>(Quantity<U> first, Quantity<U> second) where U : struct, Enum
            => first.Divide(second);

        // --- New Interface Implementation ---

        public QuantityMeasurementDto ProcessAdd(QuantityInputDto request) => ProcessOperation(request, OperationType.Add);
        public QuantityMeasurementDto ProcessSubtract(QuantityInputDto request) => ProcessOperation(request, OperationType.Subtract);
        public QuantityMeasurementDto ProcessMultiply(QuantityInputDto request) => throw new NotImplementedException("Multiply is not supported in UC16.");
        public QuantityMeasurementDto ProcessDivide(QuantityInputDto request) => ProcessOperation(request, OperationType.Divide);
        public QuantityMeasurementDto ProcessCompare(QuantityInputDto request) => ProcessOperation(request, OperationType.Compare);
        public QuantityMeasurementDto ProcessConvert(QuantityInputDto request) => ProcessOperation(request, OperationType.Convert);

        private QuantityMeasurementDto ProcessOperation(QuantityInputDto request, OperationType operation)
        {
            var response = new QuantityMeasurementDto
            {
                Category = request.Category,
                OperationType = operation.ToString(),
                IsSuccess = true,
                Message = $"{operation} processed successfully."
            };

            var entity = new QuantityMeasurementEntity
            {
                Category = request.Category,
                OperationType = operation.ToString(),
                Value1 = request.Value1,
                Unit1 = request.Unit1,
                Value2 = request.Value2 ?? 0,
                Unit2 = request.Unit2 ?? string.Empty,
                TargetUnit = request.TargetUnit ?? string.Empty
            };

            try
            {
                switch (request.Category.Trim().ToLower())
                {
                    case "length":
                        ExecuteLogic<LengthUnit>(request, response, entity, operation, ParseLengthUnit);
                        break;
                    case "weight":
                        ExecuteLogic<WeightUnit>(request, response, entity, operation, ParseWeightUnit);
                        break;
                    case "volume":
                        ExecuteLogic<VolumeUnit>(request, response, entity, operation, ParseVolumeUnit);
                        break;
                    case "temperature":
                        ExecuteLogic<TemperatureUnit>(request, response, entity, operation, ParseTemperatureUnit);
                        break;
                    default:
                        throw new ArgumentException($"Unsupported category: {request.Category}");
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"{operation} processing failed: {ex.Message}";
                entity.IsSuccess = false;
                entity.ErrorMessage = ex.Message;
            }

            if (_repository != null)
            {
                _repository.Save(entity);
            }

            return response;
        }

        private void ExecuteLogic<U>(
            QuantityInputDto request, 
            QuantityMeasurementDto response, 
            QuantityMeasurementEntity entity, 
            OperationType operation,
            Func<string, U> parseUnit) where U : struct, Enum
        {
            U unit1 = parseUnit(request.Unit1);
            var first = new Quantity<U>(request.Value1, unit1);

            if (operation == OperationType.Convert)
            {
                U tUnit = parseUnit(request.TargetUnit ?? request.Unit1);
                var converted = Convert(first, tUnit);
                response.Result = converted.ToString();
                return;
            }

            U unit2 = parseUnit(request.Unit2 ?? request.Unit1);
            var second = new Quantity<U>(request.Value2 ?? 0, unit2);

            if (operation == OperationType.Compare)
            {
                bool isEqual = AreEqual(first, second);
                response.IsEqual = isEqual;
                entity.IsEqual = isEqual;
            }
            else if (operation == OperationType.Add)
            {
                U tUnit = parseUnit(request.TargetUnit ?? request.Unit1);
                var sum = Add(first, second, tUnit);
                response.Result = sum.ToString();
                entity.AdditionResult = response.Result;
            }
            else if (operation == OperationType.Subtract)
            {
                U tUnit = parseUnit(request.TargetUnit ?? request.Unit1);
                var sub = Subtract(first, second, tUnit);
                response.Result = sub.ToString();
                entity.SubtractionResult = response.Result;
            }
            else if (operation == OperationType.Divide)
            {
                double div = Divide(first, second);
                response.DivisionResult = div;
                response.Result = div.ToString();
                entity.DivisionResult = div;
            }
            else
            {
                throw new NotSupportedException($"Operation {operation} is not supported.");
            }
        }

        // --- UC16 Parsing Helpers (Preserved exactly as is) ---
        private LengthUnit ParseLengthUnit(string unit)
        {
            switch (unit.Trim().ToLower())
            {
                case "inch": case "inches": return LengthUnit.Inch;
                case "feet": case "foot": case "ft": return LengthUnit.Feet;
                case "yard": case "yards": return LengthUnit.Yard;
                case "cm": case "centimeter": case "centimeters": return LengthUnit.Centimeter;
                default: throw new ArgumentException($"Invalid length unit: {unit}");
            }
        }

        private WeightUnit ParseWeightUnit(string unit)
        {
            switch (unit.Trim().ToLower())
            {
                case "gram": case "grams": case "g": return WeightUnit.Gram;
                case "kg": case "kilogram": case "kilograms": return WeightUnit.Kilogram;
                case "tonne": case "tonnes": case "t": return WeightUnit.Pound; // Keeping original spelling/logic
                default: throw new ArgumentException($"Invalid weight unit: {unit}");
            }
        }

        private VolumeUnit ParseVolumeUnit(string unit)
        {
            switch (unit.Trim().ToLower())
            {
                case "ml": case "millilitre": case "milliliter": case "millilitres": case "milliliters": return VolumeUnit.MILLILITRE;
                case "litre": case "liter": case "litres": case "liters": case "l": return VolumeUnit.LITRE;
                case "gallon": case "gallons": return VolumeUnit.GALLON;
                default: throw new ArgumentException($"Invalid volume unit: {unit}");
            }
        }

        private TemperatureUnit ParseTemperatureUnit(string unit)
        {
            switch (unit.Trim().ToLower())
            {
                case "c": case "celsius": case "degree celsius": return TemperatureUnit.CELSIUS;
                case "f": case "fahrenheit": case "degree fahrenheit": return TemperatureUnit.FAHRENHEIT;
                default: throw new ArgumentException($"Invalid temperature unit: {unit}");
            }
        }
    }
}