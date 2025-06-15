import axios from "axios";
import * as service from "../../services/coffeeService";
import {
  CoffeeTypeCreateDTO,
  CoffeeTypeDTO,
} from "../../interfaces/interfaces";

jest.mock("axios");
const mockedAxios = axios as jest.Mocked<typeof axios>;

describe("coffeeService", () => {
  afterEach(() => {
    jest.clearAllMocks();
  });

  it("fetchCoffeeTypes calls axios.get with correct URL", async () => {
    const data: CoffeeTypeDTO[] = [];
    mockedAxios.get.mockResolvedValueOnce({ data });
    const result = await service.fetchCoffeeTypes();
    expect(mockedAxios.get).toHaveBeenCalledWith(
      "https://localhost:7285/api/Coffee"
    );
    expect(result).toBe(data);
  });

  it("fetchCoffeeTypeById calls axios.get with correct URL", async () => {
    const data: CoffeeTypeDTO = {
      id: "1",
      name: "Test",
      coffeeIngredient: {
        dosesOfMilk: 1,
        packsOfSugar: 2,
        cinnamon: true,
        stevia: false,
        coconutMilk: false,
      },
    };
    mockedAxios.get.mockResolvedValueOnce({ data });
    const result = await service.fetchCoffeeTypeById("1");
    expect(mockedAxios.get).toHaveBeenCalledWith(
      "https://localhost:7285/api/Coffee/1"
    );
    expect(result).toBe(data);
  });

  it("deleteCoffeeType calls axios.delete with correct URL", async () => {
    mockedAxios.delete.mockResolvedValueOnce({});
    await service.deleteCoffeeType("1");
    expect(mockedAxios.delete).toHaveBeenCalledWith(
      "https://localhost:7285/api/Coffee/1"
    );
  });

  it("addCoffeeType calls axios.post with correct URL and data", async () => {
    const dto: CoffeeTypeCreateDTO = {
      name: "Test",
      coffeeIngredient: {
        dosesOfMilk: 1,
        packsOfSugar: 2,
        cinnamon: true,
        stevia: false,
        coconutMilk: false,
      },
    };
    mockedAxios.post.mockResolvedValueOnce({});
    await service.addCoffeeType(dto);
    expect(mockedAxios.post).toHaveBeenCalledWith(
      "https://localhost:7285/api/Coffee",
      dto
    );
  });

  it("updateCoffeeType calls axios.put with correct URL and data", async () => {
    const dto: CoffeeTypeDTO = {
      id: "1",
      name: "Test",
      coffeeIngredient: {
        dosesOfMilk: 1,
        packsOfSugar: 2,
        cinnamon: true,
        stevia: false,
        coconutMilk: false,
      },
    };
    mockedAxios.put.mockResolvedValueOnce({});
    await service.updateCoffeeType("1", dto);
    expect(mockedAxios.put).toHaveBeenCalledWith(
      "https://localhost:7285/api/Coffee/1",
      dto
    );
  });
});
