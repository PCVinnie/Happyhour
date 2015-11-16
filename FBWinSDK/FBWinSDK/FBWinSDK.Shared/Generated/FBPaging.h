

//------------------------------------------------------------------------------
//
// Copyright (c) 2015 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//------------------------------------------------------------------------------




#pragma once
//------------------------------------------------------------------------------
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//------------------------------------------------------------------------------

#include "JsonClassFactory.h"

#include "FBCursors.h"


namespace Facebook
{
    namespace Graph
    {

        public ref class FBPaging sealed 
        {
            public:

                static Object^ FromJson(
                    Platform::String^ JsonText 
                    );


                property Facebook::Graph::FBCursors^ Cursors
                {
                    Facebook::Graph::FBCursors^ get();
                    void set(Facebook::Graph::FBCursors^ value);
                }


                property Platform::String^ Next
                {
                    Platform::String^ get();
                    void set(Platform::String^ value);
                }


                property Platform::String^ Previous
                {
                    Platform::String^ get();
                    void set(Platform::String^ value);
                }


            private:
                FBPaging(
                    );


                Facebook::Graph::FBCursors^ _cursors;

                Platform::String^ _next;

                Platform::String^ _previous;

        };
    }
}


