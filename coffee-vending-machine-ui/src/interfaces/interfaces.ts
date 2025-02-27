export interface CoffeeIngredientDTO {
  dosesOfMilk: number;
  packsOfSugar: number;
  cinnamon: boolean;
  stevia: boolean;
  coconutMilk: boolean;
}

export interface CoffeeTypeDTO {
  id: string;
  name: string;
  coffeeIngredient: CoffeeIngredientDTO;
}

export interface CoffeeTypeCreateDTO {
  name: string;
  coffeeIngredient: CoffeeIngredientDTO;
}

export interface ConfirmDeleteModalProps {
  show: boolean;
  onHide: () => void;
  onConfirm: () => void;
}

export interface ConfirmNavigationModalProps {
  show: boolean;
  onHide: () => void;
  onConfirm: () => void;
}
