import axios from "axios";
import { Formik, Field, Form as FormikForm, ErrorMessage } from "formik";
import React, { useEffect, useState } from "react";
import { Container, Button, Row, Col, Form } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import ConfirmNavigationModal from "./modals/ConfirmNavigationModal";
import { validationSchema } from "../utils/formUtils";
import LoadingSpinner from "./loader/LoadingSpinner";
import { fetchCoffeeTypeById } from "../services/coffeeService";

const EditCoffeeType: React.FC = () => {
  const [showConfirmModal, setShowConfirmModal] = useState<boolean>(false);
  const [initialValues, setInitialValues] = useState({
    name: "",
    dosesOfMilk: 0,
    packsOfSugar: 0,
    cinnamon: false,
    stevia: false,
    coconutMilk: false,
  });
  const [loading, setLoading] = useState<boolean>(true);
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();

  useEffect(() => {
    const loadCoffeeType = async () => {
      try {
        const coffeeType = await fetchCoffeeTypeById(id!);
        setInitialValues({
          name: coffeeType.name,
          dosesOfMilk: coffeeType.coffeeIngredient.dosesOfMilk,
          packsOfSugar: coffeeType.coffeeIngredient.packsOfSugar,
          cinnamon: coffeeType.coffeeIngredient.cinnamon,
          stevia: coffeeType.coffeeIngredient.stevia,
          coconutMilk: coffeeType.coffeeIngredient.coconutMilk,
        });
        setLoading(false);
      } catch (error) {
        console.error("There was an error fetching the coffee type!", error);
        setLoading(false);
      }
    };

    loadCoffeeType();
  }, [id]);

  const onSubmit = (values: typeof initialValues, { setSubmitting }: any) => {
    axios
      .put(`https://localhost:7285/api/Coffee/${id}`, {
        id: id,
        name: values.name,
        coffeeIngredient: {
          dosesOfMilk: values.dosesOfMilk,
          packsOfSugar: values.packsOfSugar,
          cinnamon: values.cinnamon,
          stevia: values.stevia,
          coconutMilk: values.coconutMilk,
        },
      })
      .then((response) => {
        console.log("Coffee type updated successfully", response);
        setSubmitting(false);
        navigate("/coffees");
      })
      .catch((error) => {
        console.error("There was an error updating the coffee type!", error);
        setSubmitting(false);
      });
  };

  const handleGoBack = () => {
    setShowConfirmModal(true);
  };

  const confirmNavigation = () => {
    setShowConfirmModal(false);
    navigate(-1); // Go back to the previous page
  };

  if (loading) {
    return <LoadingSpinner />;
  }

  return (
    <Container className="mt-5">
      <Row>
        <Col md={{ span: 8, offset: 2 }}>
          <h2>Edit Coffee Type</h2>
        </Col>
      </Row>
      <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={onSubmit}
        enableReinitialize
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
                  <Form.Label htmlFor="dosesOfMilk">Doses of Milk</Form.Label>
                  <Field
                    name="dosesOfMilk"
                    type="number"
                    className="form-control"
                  />
                  <ErrorMessage
                    name="dosesOfMilk"
                    component="div"
                    className="text-danger"
                  />
                </Form.Group>
              </Col>
            </Row>
            <Row className="mt-3">
              <Col md={{ span: 8, offset: 2 }}>
                <Form.Group className="mb-3">
                  <Form.Label htmlFor="packsOfSugar">Packs of Sugar</Form.Label>
                  <Field
                    name="packsOfSugar"
                    type="number"
                    className="form-control"
                  />
                  <ErrorMessage
                    name="packsOfSugar"
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
                    name="cinnamon"
                    as={Field}
                  />
                  <ErrorMessage
                    name="cinnamon"
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
                    name="stevia"
                    as={Field}
                  />
                  <ErrorMessage
                    name="stevia"
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
                    name="coconutMilk"
                    as={Field}
                  />
                  <ErrorMessage
                    name="coconutMilk"
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
                  Update Coffee
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

export default EditCoffeeType;
