// Copyright 2009 the Sputnik authors.  All rights reserved.
// This code is governed by the BSD license found in the LICENSE file.

/**
* @name: S15.4.4.11_A7.2;
* @section: 15.4.4.11, 15.2.4.5, 11.4.1;
* @assertion: The length property of sort has the attribute DontDelete;
* @description: Checking use hasOwnProperty, delete;
* @strict_mode_negative
*/


// Converted for Test262 from original Sputnik source

ES5Harness.registerTest( {
id: "S15.4.4.11_A7.2",

path: "TestCases/15_Native/15.4_Array_Objects/15.4.4_Properties_of_the_Array_Prototype_Object/15.4.4.11_Array_prototype_sort/S15.4.4.11_A7.2.js",

assertion: "The length property of sort has the attribute DontDelete",

description: "Checking use hasOwnProperty, delete",

test: function testcase() {
   //CHECK#1
if (Array.prototype.sort.hasOwnProperty('length') !== true) {
  $FAIL('#1: Array.sort.prototype.hasOwnProperty(\'length\') === true. Actual: ' + (Array.sort.prototype.hasOwnProperty('length')));
}

delete Array.prototype.sort.length;

//CHECK#2
if (Array.prototype.sort.hasOwnProperty('length') !== true) {
  $ERROR('#2: delete Array.prototype.sort.length; Array.prototype.sort.hasOwnProperty(\'length\') === true. Actual: ' + (Array.prototype.sort.hasOwnProperty('length')));
}

//CHECK#3
if (Array.prototype.sort.length === undefined) {
  $ERROR('#3: delete Array.prototype.sort.length; Array.prototype.sort.length !== undefined');
}



 }
});
