import * as Yup from "yup";

export const validationSchema = Yup.object({
  name: Yup.string().required("Name is required"),
  coffeeIngredient: Yup.object({
    dosesOfMilk: Yup.number()
      .required("Doses of Milk is required")
      .min(0, "Doses of Milk must be at least 0")
      .max(5, "Doses of Milk must be at most 5"),
    packsOfSugar: Yup.number()
      .required("Packs of Sugar is required")
      .min(0, "Packs of Sugar must be at least 0")
      .max(5, "Packs of Sugar must be at most 5"),
    cinnamon: Yup.boolean().required("Cinnamon is required"),
    stevia: Yup.boolean().required("Stevia is required"),
    coconutMilk: Yup.boolean().required("Coconut Milk is required"),
  }),
});
