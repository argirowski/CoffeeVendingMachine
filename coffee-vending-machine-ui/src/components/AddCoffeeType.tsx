import { Formik, Field, Form as FormikForm, ErrorMessage } from "formik";
import React, { useState } from "react";
import { Container, Button, Row, Col, Form } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import ConfirmNavigationModal from "./modals/ConfirmNavigationModal";
import { validationSchema } from "../utils/formUtils";
import { addCoffeeType } from "../services/coffeeService";
import { CoffeeTypeCreateDTO } from "../interfaces/interfaces";

const AddCoffeeType: React.FC = () => {
  const [showConfirmModal, setShowConfirmModal] = useState<boolean>(false);
  const navigate = useNavigate();

  const initialValues: CoffeeTypeCreateDTO = {
    name: "",
    coffeeIngredient: {
      dosesOfMilk: 0,
      packsOfSugar: 0,
      cinnamon: false,
      stevia: false,
      coconutMilk: false,
    },
  };

  const onSubmit = async (
    values: CoffeeTypeCreateDTO,
    { setSubmitting }: { setSubmitting: (isSubmitting: boolean) => void }
  ) => {
    try {
      await addCoffeeType(values);
      console.log("Coffee type added successfully");
      setSubmitting(false);
      navigate("/coffees");
    } catch (error) {
      console.error("There was an error adding the coffee type!", error);
      setSubmitting(false);
    }
  };

  const handleGoBack = () => {
    setShowConfirmModal(true);
  };

  const confirmNavigation = () => {
    setShowConfirmModal(false);
    navigate(-1);
  };

  return (
    <Container className="mt-5">
      <Row>
        <Col md={{ span: 8, offset: 2 }}>
          <h2>Add Coffee Type</h2>
        </Col>
      </Row>
      <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={onSubmit}
      >
        {({ isSubmitting, dirty }) => (
          <FormikForm>
            <Row className="mt-3">
              <Col md={{ span: 8, offset: 2 }}>
                <Form.Group className="mb-3">
                  <Form.Label htmlFor="name">Name</Form.Label>
                  <Field name="name" type="text" className="form-control" />
                  <ErrorMessage
                    name="name"
                    component="div"
                    className="text-danger"
                  />
                </Form.Group>
              </Col>
            </Row>
            <Row className="mt-3">
              <Col md={{ span: 8, offset: 2 }}>
                <Form.Group className="mb-3">
                  <Form.Label htmlFor="coffeeIngredient.dosesOfMilk">
                    Doses of Milk
                  </Form.Label>
                  <Field
                    name="coffeeIngredient.dosesOfMilk"
                    type="number"
                    className="form-control"
                  />
                  <ErrorMessage
                    name="coffeeIngredient.dosesOfMilk"
                    component="div"
                    className="text-danger"
                  />
                </Form.Group>
              </Col>
            </Row>
            <Row className="mt-3">
              <Col md={{ span: 8, offset: 2 }}>
                <Form.Group className="mb-3">
                  <Form.Label htmlFor="coffeeIngredient.packsOfSugar">
                    Packs of Sugar
                  </Form.Label>
                  <Field
                    name="coffeeIngredient.packsOfSugar"
                    type="number"
                    className="form-control"
                  />
                  <ErrorMessage
                    name="coffeeIngredient.packsOfSugar"
                    component="div"
                    className="text-danger"
                  />
                </Form.Group>
              </Col>
            </Row>
            <Row className="mt-3">
              <Col md={{ span: 8, offset: 2 }}>
                <Form.Group className="mb-3">
                  <Form.Check
                    type="checkbox"
                    label="Cinnamon"
                    name="coffeeIngredient.cinnamon"
                    as={Field}
                  />
                  <ErrorMessage
                    name="coffeeIngredient.cinnamon"
                    component="div"
                    className="text-danger"
                  />
                </Form.Group>
              </Col>
            </Row>
            <Row className="mt-3">
              <Col md={{ span: 8, offset: 2 }}>
                <Form.Group className="mb-3">
                  <Form.Check
                    type="checkbox"
                    label="Stevia"
                    name="coffeeIngredient.stevia"
                    as={Field}
                  />
                  <ErrorMessage
                    name="coffeeIngredient.stevia"
                    component="div"
                    className="text-danger"
                  />
                </Form.Group>
              </Col>
            </Row>
            <Row className="mt-3">
              <Col md={{ span: 8, offset: 2 }}>
                <Form.Group className="mb-3">
                  <Form.Check
                    type="checkbox"
                    label="Coconut Milk"
                    name="coffeeIngredient.coconutMilk"
                    as={Field}
                  />
                  <ErrorMessage
                    name="coffeeIngredient.coconutMilk"
                    component="div"
                    className="text-danger"
                  />
                </Form.Group>
              </Col>
            </Row>
            <Row className="mt-3">
              <Col
                md={{ span: 8, offset: 2 }}
                className="d-flex justify-content-start"
              >
                <Button
                  variant="primary"
                  type="submit"
                  className="me-3"
                  size="lg"
                  disabled={isSubmitting}
                >
                  Add Coffee
                </Button>
                <Button
                  variant="secondary"
                  size="lg"
                  onClick={dirty ? handleGoBack : () => navigate(-1)}
                >
                  Go Back
                </Button>
              </Col>
            </Row>
          </FormikForm>
        )}
      </Formik>
      <ConfirmNavigationModal
        show={showConfirmModal}
        onHide={() => setShowConfirmModal(false)}
        onConfirm={confirmNavigation}
      />
    </Container>
  );
};

export default AddCoffeeType;
