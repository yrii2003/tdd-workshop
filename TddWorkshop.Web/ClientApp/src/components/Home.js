import React, {useEffect, useState} from 'react';
import {AutoForm} from "uniforms-material";
import Ajv from 'ajv';
import {JSONSchemaBridge} from 'uniforms-bridge-json-schema';


function createValidator(schema, ajv) {
    const validator = ajv.compile(schema);

    return (model) => {
        validator(model);
        return validator.errors?.length ? {details: validator.errors} : null;
    };
}

function adaptSchema(parameters){
    const schema = {
        title: 'Form',
        type: 'object',
        properties: {},
        required: []
    };
    
    const iKeys = Object.keys(parameters);
    for(let i = 0; i < iKeys.length; i++) {
        const jKeys = Object.keys(parameters[i]);
        for (let j = 0; j < jKeys.length; j++) {
            const key = i + '_' + j;
            schema.properties[key] = {
                type: 'string',
                id: key
            };
        }
    }
    const ajv = new Ajv({allErrors: true, useDefaults: true});
    const schemaValidator = createValidator(schema, ajv);
    return new JSONSchemaBridge(schema, schemaValidator);
}

const baseUrl = 'https://localhost:5001';
const swaggerUrl = baseUrl + '/swagger/v1/swagger.json';

function onSubmit(data) {
    console.log(data);
    fetch(baseUrl + '/Calculator/Calculate', {method: 'POST', body: JSON.stringify(data)})
        .then(response => response.json())
        .then(json => {
            console.log(123)
        });
}

export function Home (){
    const [schema, setSchema] = useState(null);
    
    useEffect(() => {
        fetch(swaggerUrl)
            .then(response => response.json())
            .then(json => {
                const PersonalInfo = json['components']['schemas']['PersonalInfo'];
                const CreditInfo = json['components']['schemas']['CreditInfo'];
                const PassportInfo = json['components']['schemas']['PassportInfo'];

                setSchema(adaptSchema({
                    PersonalInfo, CreditInfo, PassportInfo
                }));
            })
    }, []);
    
    return !schema 
        ? <div>Loading</div>
        : <AutoForm schema={schema} onSubmit={onSubmit}/>
}