import axios from "axios";
import { CoffeeTypeCreateDTO, CoffeeTypeDTO } from "../interfaces/interfaces";

const API_URL = "https://localhost:7285/api/Coffee";

export const fetchCoffeeTypes = async (): Promise<CoffeeTypeDTO[]> => {
  const response = await axios.get(API_URL);
  return response.data;
};

export const fetchCoffeeTypeById = async (
  id: string
): Promise<CoffeeTypeDTO> => {
  const response = await axios.get(`${API_URL}/${id}`);
  return response.data;
};

export const deleteCoffeeType = async (id: string): Promise<void> => {
  await axios.delete(`${API_URL}/${id}`);
};

export const addCoffeeType = async (
  coffeeType: CoffeeTypeCreateDTO
): Promise<void> => {
  await axios.post(API_URL, coffeeType);
};

export const updateCoffeeType = async (
  id: string,
  coffeeType: CoffeeTypeDTO
): Promise<void> => {
  await axios.put(`${API_URL}/${id}`, coffeeType);
};
