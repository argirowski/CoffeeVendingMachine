import { validationSchema } from "../../utils/formUtils";

describe("validationSchema", () => {
  it("validates a correct object", async () => {
    const valid = {
      name: "Test Coffee",
      coffeeIngredient: {
        dosesOfMilk: 2,
        packsOfSugar: 1,
        cinnamon: true,
        stevia: false,
        coconutMilk: false,
      },
    };
    await expect(validationSchema.validate(valid)).resolves.toBeTruthy();
  });

  it("fails when name is missing", async () => {
    const invalid = {
      coffeeIngredient: {
        dosesOfMilk: 2,
        packsOfSugar: 1,
        cinnamon: true,
        stevia: false,
        coconutMilk: false,
      },
    };
    await expect(validationSchema.validate(invalid)).rejects.toThrow(
      /name is required/i
    );
  });

  it("fails when dosesOfMilk is out of range", async () => {
    const invalid = {
      name: "Test Coffee",
      coffeeIngredient: {
        dosesOfMilk: 10,
        packsOfSugar: 1,
        cinnamon: true,
        stevia: false,
        coconutMilk: false,
      },
    };
    await expect(validationSchema.validate(invalid)).rejects.toThrow(
      /doses of milk must be at most 5/i
    );
  });

  it("fails when packsOfSugar is negative", async () => {
    const invalid = {
      name: "Test Coffee",
      coffeeIngredient: {
        dosesOfMilk: 2,
        packsOfSugar: -1,
        cinnamon: true,
        stevia: false,
        coconutMilk: false,
      },
    };
    await expect(validationSchema.validate(invalid)).rejects.toThrow(
      /packs of sugar must be at least 0/i
    );
  });

  it("fails when a boolean is missing", async () => {
    const invalid = {
      name: "Test Coffee",
      coffeeIngredient: {
        dosesOfMilk: 2,
        packsOfSugar: 1,
        // cinnamon missing
        stevia: false,
        coconutMilk: false,
      },
    };
    await expect(validationSchema.validate(invalid)).rejects.toThrow(
      /cinnamon is required/i
    );
  });
});
