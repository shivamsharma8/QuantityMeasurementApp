using System;
using QuantityMeasurementModelLayer;
using QuantityMeasurementRepositoryLayer;

namespace QuantityMeasurementBusinessLayer
{
    /// <summary>
    /// Service layer for quantity measurement operations.
    /// Acts as a facade between presentation layer and domain logic.
    /// </summary>
    public class QuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repository;

        public QuantityMeasurementService()
        {
        }

        /// <summary>
        /// Constructor with repository injection (UC15 N-Tier).
        /// </summary>
        public QuantityMeasurementService(IQuantityMeasurementRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Checks whether two quantities are equal after converting to base unit.
        /// </summary>
        public bool AreEqual<U>(Quantity<U> first, Quantity<U> second) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return first.Equals(second);
        }

        /// <summary>
        /// Converts a quantity into the target unit.
        /// </summary>
        public Quantity<U> Convert<U>(Quantity<U> quantity, U targetUnit) where U : struct, Enum
        {
            if (quantity == null)
                throw new ArgumentException("Quantity must not be null.");

            return quantity.Convert(targetUnit);
        }

        /// <summary>
        /// Adds two quantities and returns result in first quantity's unit.
        /// </summary>
        public Quantity<U> Add<U>(Quantity<U> first, Quantity<U> second) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return Quantity<U>.Add(first, second);
        }

        /// <summary>
        /// Adds two quantities and returns result in target unit.
        /// </summary>
        public Quantity<U> Add<U>(Quantity<U> first, Quantity<U> second, U targetUnit) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return Quantity<U>.Add(first, second, targetUnit);
        }

        /// <summary>
        /// Subtracts second quantity from first and returns result in first quantity's unit.
        /// </summary>
        public Quantity<U> Subtract<U>(Quantity<U> first, Quantity<U> second) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return Quantity<U>.Subtract(first, second);
        }

        /// <summary>
        /// Subtracts second quantity from first and returns result in target unit.
        /// </summary>
        public Quantity<U> Subtract<U>(Quantity<U> first, Quantity<U> second, U targetUnit) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return Quantity<U>.Subtract(first, second, targetUnit);
        }

        /// <summary>
        /// Divides first quantity by second and returns ratio.
        /// </summary>
        public double Divide<U>(Quantity<U> first, Quantity<U> second) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return first.Divide(second);
        }

        /// <summary>
        /// Main UC15 DTO-based processing entry point.
        /// Controller will call this.
        /// </summary>
        public MeasurementResponseDTO ProcessMeasurement(MeasurementRequestDTO request)
        {
            if (request == null)
            {
                return new MeasurementResponseDTO
                {
                    IsSuccess = false,
                    Message = "Request cannot be null."
                };
            }

            if (string.IsNullOrWhiteSpace(request.Category))
            {
                return new MeasurementResponseDTO
                {
                    IsSuccess = false,
                    Message = "Category is required."
                };
            }

            switch (request.Category.Trim().ToLower())
            {
                case "length":
                    return ProcessLength(request);

                case "weight":
                    return ProcessWeight(request);

                case "volume":
                    return ProcessVolume(request);

                case "temperature":
                    return ProcessTemperature(request);

                default:
                    return new MeasurementResponseDTO
                    {
                        IsSuccess = false,
                        Message = $"Unsupported category: {request.Category}"
                    };
            }
        }

        /// <summary>
        /// Handles length measurements.
        /// </summary>
        private MeasurementResponseDTO ProcessLength(MeasurementRequestDTO request)
        {
            try
            {
                LengthUnit unit1 = ParseLengthUnit(request.Unit1);
                LengthUnit unit2 = ParseLengthUnit(request.Unit2);
                LengthUnit targetUnit = ParseLengthUnit(request.TargetUnit);

                var first = new Quantity<LengthUnit>(request.Value1, unit1);
                var second = new Quantity<LengthUnit>(request.Value2, unit2);

                bool isEqual = AreEqual(first, second);
                var addition = Add(first, second, targetUnit);
                var subtraction = Subtract(first, second, targetUnit);
                double division = Divide(first, second);

                var response = new MeasurementResponseDTO
                {
                    IsSuccess = true,
                    Category = "Length",
                    AdditionResult = addition.ToString(),
                    SubtractionResult = subtraction.ToString(),
                    DivisionResult = division,
                    IsEqual = isEqual,
                    Message = "Length measurement processed successfully."
                };

                SaveMeasurement(request, response);
                return response;
            }
            catch (Exception ex)
            {
                return new MeasurementResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Length processing failed: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Handles weight measurements.
        /// </summary>
        private MeasurementResponseDTO ProcessWeight(MeasurementRequestDTO request)
        {
            try
            {
                WeightUnit unit1 = ParseWeightUnit(request.Unit1);
                WeightUnit unit2 = ParseWeightUnit(request.Unit2);
                WeightUnit targetUnit = ParseWeightUnit(request.TargetUnit);

                var first = new Quantity<WeightUnit>(request.Value1, unit1);
                var second = new Quantity<WeightUnit>(request.Value2, unit2);

                bool isEqual = AreEqual(first, second);
                var addition = Add(first, second, targetUnit);
                var subtraction = Subtract(first, second, targetUnit);
                double division = Divide(first, second);

                var response = new MeasurementResponseDTO
                {
                    IsSuccess = true,
                    Category = "Weight",
                    AdditionResult = addition.ToString(),
                    SubtractionResult = subtraction.ToString(),
                    DivisionResult = division,
                    IsEqual = isEqual,
                    Message = "Weight measurement processed successfully."
                };

                SaveMeasurement(request, response);
                return response;
            }
            catch (Exception ex)
            {
                return new MeasurementResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Weight processing failed: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Handles volume measurements.
        /// </summary>
        private MeasurementResponseDTO ProcessVolume(MeasurementRequestDTO request)
        {
            try
            {
                VolumeUnit unit1 = ParseVolumeUnit(request.Unit1);
                VolumeUnit unit2 = ParseVolumeUnit(request.Unit2);
                VolumeUnit targetUnit = ParseVolumeUnit(request.TargetUnit);

                var first = new Quantity<VolumeUnit>(request.Value1, unit1);
                var second = new Quantity<VolumeUnit>(request.Value2, unit2);

                bool isEqual = AreEqual(first, second);
                var addition = Add(first, second, targetUnit);
                var subtraction = Subtract(first, second, targetUnit);
                double division = Divide(first, second);

                var response = new MeasurementResponseDTO
                {
                    IsSuccess = true,
                    Category = "Volume",
                    AdditionResult = addition.ToString(),
                    SubtractionResult = subtraction.ToString(),
                    DivisionResult = division,
                    IsEqual = isEqual,
                    Message = "Volume measurement processed successfully."
                };

                SaveMeasurement(request, response);
                return response;
            }
            catch (Exception ex)
            {
                return new MeasurementResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Volume processing failed: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Handles temperature measurements.
        /// </summary>
        private MeasurementResponseDTO ProcessTemperature(MeasurementRequestDTO request)
        {
            try
            {
                TemperatureUnit unit1 = ParseTemperatureUnit(request.Unit1);
                TemperatureUnit unit2 = ParseTemperatureUnit(request.Unit2);
                TemperatureUnit targetUnit = ParseTemperatureUnit(request.TargetUnit);

                var first = new Quantity<TemperatureUnit>(request.Value1, unit1);
                var second = new Quantity<TemperatureUnit>(request.Value2, unit2);

                bool isEqual = AreEqual(first, second);
                var addition = Add(first, second, targetUnit);
                var subtraction = Subtract(first, second, targetUnit);
                double division = Divide(first, second);

                var response = new MeasurementResponseDTO
                {
                    IsSuccess = true,
                    Category = "Temperature",
                    AdditionResult = addition.ToString(),
                    SubtractionResult = subtraction.ToString(),
                    DivisionResult = division,
                    IsEqual = isEqual,
                    Message = "Temperature measurement processed successfully."
                };

                SaveMeasurement(request, response);
                return response;
            }
            catch (Exception ex)
            {
                return new MeasurementResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Temperature processing failed: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Saves measurement result into repository if repository is available.
        /// </summary>
        private void SaveMeasurement(MeasurementRequestDTO request, MeasurementResponseDTO response)
        {
            if (_repository == null)
                return;

            var entity = new QuantityMeasurementEntity
            {
                Category = request.Category,
                Value1 = request.Value1,
                Unit1 = request.Unit1,
                Value2 = request.Value2,
                Unit2 = request.Unit2,
                TargetUnit = request.TargetUnit,
                AdditionResult = response.AdditionResult,
                SubtractionResult = response.SubtractionResult,
                DivisionResult = response.DivisionResult,
                IsEqual = response.IsEqual
            };

            _repository.Save(entity);
        }

        // -------------------------
        // Unit parsing helpers
        // -------------------------

        private LengthUnit ParseLengthUnit(string unit)
        {
            switch (unit.Trim().ToLower())
            {
                case "inch":
                case "inches":
                    return LengthUnit.Inch;

                case "feet":
                case "foot":
                case "ft":
                    return LengthUnit.Feet;

                case "yard":
                case "yards":
                    return LengthUnit.Yard;

                case "cm":
                case "centimeter":
                case "centimeters":
                    return LengthUnit.Centimeter;

                default:
                    throw new ArgumentException($"Invalid length unit: {unit}");
            }
        }

        private WeightUnit ParseWeightUnit(string unit)
        {
            switch (unit.Trim().ToLower())
            {
                case "gram":
                case "grams":
                case "g":
                    return WeightUnit.Gram;

                case "kg":
                case "kilogram":
                case "kilograms":
                    return WeightUnit.Kilogram;

                case "tonne":
                case "tonnes":
                case "t":
                    return WeightUnit.Pound;

                default:
                    throw new ArgumentException($"Invalid weight unit: {unit}");
            }
        }

        private VolumeUnit ParseVolumeUnit(string unit)
        {
            switch (unit.Trim().ToLower())
            {
                case "ml":
                case "millilitre":
                case "milliliter":
                case "millilitres":
                case "milliliters":
                    return VolumeUnit.MILLILITRE;

                case "litre":
                case "liter":
                case "litres":
                case "liters":
                case "l":
                    return VolumeUnit.LITRE;

                case "gallon":
                case "gallons":
                    return VolumeUnit.GALLON;

                default:
                    throw new ArgumentException($"Invalid volume unit: {unit}");
            }
        }

        private TemperatureUnit ParseTemperatureUnit(string unit)
        {
            switch (unit.Trim().ToLower())
            {
                case "c":
                case "celsius":
                case "degree celsius":
                    return TemperatureUnit.CELSIUS;

                case "f":
                case "fahrenheit":
                case "degree fahrenheit":
                    return TemperatureUnit.FAHRENHEIT;

                default:
                    throw new ArgumentException($"Invalid temperature unit: {unit}");
            }
        }
    }
}